using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TkdAPI.Entities;
using TkdScoringApp.API.Dto;
using TkdScoringApp.API.Entities;
using Microsoft.AspNetCore.SignalR;
using TkdScoringApp.API.iService;
using TkdAPI.Dto;
using System.Collections.Generic;

namespace TkdScoringApp.API.Controllers
{
    [ApiController]
    [Route("match")]
    public class MatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly iTkdRepo _repo;
        private readonly iScoring _scoring;

        private readonly iUser _user;

        private IHubContext<ChartHub> _hub;


        public MatchController(
               IMapper mapper,
               iTkdRepo repo,
               IHubContext<ChartHub> hub,
               iScoring scoring,
               iUser user
               )
        {
            _repo = repo;
            _scoring = scoring;
            _mapper = mapper;
            _hub = hub;
            _user = user;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMatch(MatchDto match)
        {
            var newMatch = _mapper.Map<Match>(match);
            newMatch.isPause = true;
            var ringAvailability = await _repo.checkWhetherRingAvailable(match.RingId);
            if (ringAvailability != null)
            {
                return BadRequest(new { message = "Selected Ring Not Available" });
            }
            _repo.Add(newMatch);
            if (await _repo.Save())
            {
                return Ok(newMatch);
            }
            return BadRequest();
        }

        [HttpPost("status/{status}")]
        [AllowAnonymous]
        public async Task<IActionResult> ModifyMatchStatus(string status, MatchDto match)
        {
            var newMatch = _mapper.Map<Match>(match);
            var matchRecord = await _repo.GetMatch(newMatch.Id);
            var obj = new { matchStart = true, matchBreak = false, matchId = newMatch.Id };
            if (status == "start" || status == "resume")
            {
                matchRecord.isPause = false;
            }
            else
            {
                if (status == "pause")
                {
                    matchRecord.isPause = true;
                    obj = new { matchStart = false, matchBreak = false, matchId = newMatch.Id };
                }
                else
                {
                    if (status == "break")
                    {
                        matchRecord.isPause = true;
                        obj = new { matchStart = false, matchBreak = true, matchId = newMatch.Id };
                    }
                    else
                    {
                        return BadRequest(new { message = "Status not found" });
                    }
                }
            }



            _repo.updateMatch(matchRecord);


            if (await _repo.Save())
            {
                await _hub.Clients.All.SendAsync("transferData", obj);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("end")]
        [AllowAnonymous]
        public async Task<IActionResult> EndMatch(MatchDto match)
        {
            var newMatch = _mapper.Map<Match>(match);
            var matchRecord = await _repo.GetMatch(newMatch.Id);

            matchRecord.isFinished = true;
            matchRecord.isPause = false;

            _repo.updateMatch(matchRecord);

            if (await _repo.Save())
            {
                await _hub.Clients.All.SendAsync("matchEnd", matchRecord);

                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("kickhead")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateKickHead(ScoreDto score)
        {

            var newscore = _mapper.Map<Kickhead>(score);
            var player = await _user.GetPlayer(newscore.PlayerId);
            var obj = new { score = newscore, player = player };
            await _hub.Clients.All.SendAsync("judgeTempUpdate", obj);
            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;

                    _repo.Add(newscore);

                    if (await _repo.Save())
                    {

                        return Ok();
                    }

                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                newscore.NoOfConfirmation += 1;

                _repo.Add(newscore);

                if (await _repo.Save())
                {
                    return Ok();
                }

                return BadRequest();
            }
        }

        [HttpPost("kickbody")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateKickbody(ScoreDto score)
        {
            var newscore = _mapper.Map<KickBody>(score);
            var player = await _user.GetPlayer(newscore.PlayerId);
            var obj = new { score = newscore, player = player };
            await _hub.Clients.All.SendAsync("judgeTempUpdate", obj);
            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    if (await _repo.Save())
                    {

                        return Ok();
                    }
                }
                return Ok();
            }
            else
            {
                newscore.NoOfConfirmation += 1;

                _repo.Add(newscore);

                if (await _repo.Save())
                {
                    return Ok();
                }

                return BadRequest();
            }
        }

        [HttpPost("punch")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePunch(ScoreDto score)
        {
            var newscore = _mapper.Map<Punch>(score);
            var player = await _user.GetPlayer(newscore.PlayerId);
            var obj = new { score = newscore, player = player };
            await _hub.Clients.All.SendAsync("judgeTempUpdate", obj);
            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    if (await _repo.Save())
                    {

                        return Ok();
                    }
                }
                return Ok();
            }
            else
            {
                newscore.NoOfConfirmation += 1;

                _repo.Add(newscore);

                if (await _repo.Save())
                {

                    return Ok();
                }

                return BadRequest();
            }
        }

        [HttpPost("turningkickbody")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateTurningKickbody(ScoreDto score)
        {
            var newscore = _mapper.Map<TurningKickBody>(score);
            var player = await _user.GetPlayer(newscore.PlayerId);
            var obj = new { score = newscore, player = player };
            await _hub.Clients.All.SendAsync("judgeTempUpdate", obj);

            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    if (await _repo.Save())
                    {

                        return Ok();
                    }
                }
                return Ok();
            }
            else
            {
                newscore.NoOfConfirmation += 1;

                _repo.Add(newscore);

                if (await _repo.Save())
                {
                    return Ok();
                }

                return BadRequest();
            }
        }

        [HttpPost("turningkickhead")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateTurningKickHead(ScoreDto score)
        {
            var newscore = _mapper.Map<TurningKickHead>(score);
            var player = await _user.GetPlayer(newscore.PlayerId);
            var obj = new { score = newscore, player = player };
            await _hub.Clients.All.SendAsync("judgeTempUpdate", obj);
            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    if (await _repo.Save())
                    {

                        return Ok();
                    }
                }
                return Ok();
            }
            else
            {
                newscore.NoOfConfirmation += 1;

                _repo.Add(newscore);

                if (await _repo.Save())
                {

                    return Ok();
                }

                return BadRequest();
            }
        }

        [HttpPost("foul")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateFoul(FoulDto foul)
        {
            var newfoul = _mapper.Map<Foul>(foul);

            var updatefoul = await _scoring.UpdateFoul(newfoul);

            if (updatefoul)
            {
                _repo.Add(newfoul);
                if (await _repo.Save())
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("score")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateScore(ScoreDto score)
        {
            var newscore = _mapper.Map<Score>(score);
            newscore.Type = "force";
            var update = await _scoring.UpdateScoreManual(newscore);

            if (update)
            {
                _repo.Add(newscore);
                if (await _repo.Save())
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet("summary/{matchId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMatchSummary(int matchId)
        {
            var match = await _scoring.GetMatch(matchId);

            if (match == null)
            {
                return BadRequest(new { message = " There is no Such match" });
            }

            IList<Score> PlayerREDScore = new List<Score>();
            IList<Score> PlayerBLUEScore = new List<Score>();
            IList<Foul> PlayerREDFoul = new List<Foul>();
            IList<Foul> PlayerBLUEFoul = new List<Foul>();
            Player playerRED = new Player();
            Player playerBLUE = new Player();

            foreach (var item in match.Players)
            {
                if (item.Color == "red")
                {   
                    playerRED = item;
                    PlayerREDScore = await _scoring.GetScoresOfMatch(matchId, item.id);
                    PlayerREDFoul = await _scoring.GetFoulOfMatch(matchId, item.id);
                }

                if (item.Color == "blue")
                {

                    playerBLUE = item;
                    PlayerBLUEScore = await _scoring.GetScoresOfMatch(matchId, item.id);
                    PlayerBLUEFoul = await _scoring.GetFoulOfMatch(matchId, item.id);
                }


            }

            return Ok(new
            {
                PlayerRED = playerRED,
                playerBLUE = playerBLUE,
                PlayerREDScore = PlayerREDScore,
                PlayerREDFoul = PlayerREDFoul,
                PlayerBLUEScore = PlayerBLUEScore,
                PlayerBLUEFoul = PlayerBLUEFoul
            }
          );
        }

        [HttpGet("{matchId}")]
        [AllowAnonymous]
        public async Task<IActionResult> MatchSummary(int matchId)
        {
            var match = await _scoring.GetMatch(matchId);

            if (match == null)
            {
                return BadRequest(new { message = " There is no Such match" });
            }

            IList<Score> Score = new List<Score>();
            IList<Foul> Foul = new List<Foul>();

            Score = await _scoring.GetScore(matchId);
            Foul = await _scoring.GetFoul(matchId);

            return Ok(new
            {
                score = Score,
                foul = Foul
            }
          );
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMatches()
        {
            var match = await _scoring.GetAllMatches();


            return Ok(match);
        }

    }
}
