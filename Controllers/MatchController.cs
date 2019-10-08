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

        private IHubContext<ChartHub> _hub;


        public MatchController(
               IMapper mapper,
               iTkdRepo repo,
               IHubContext<ChartHub> hub,
               iScoring scoring
               )
        {
            _repo = repo;
            _scoring = scoring;
            _mapper = mapper;
            _hub = hub;
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

            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    return Ok();
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

            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    return Ok();
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

            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    return Ok();
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

            if (await _scoring.HasRecord(newscore))
            {
                var updatescore = await _scoring.UpdateScore(newscore);

                if (updatescore != null)
                {
                    newscore.NoOfConfirmation = updatescore.NoOfConfirmation;
                    _repo.Add(newscore);
                    return Ok();
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
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("score")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateScore(ScoreDto score)
        {
            var newscore = _mapper.Map<Score>(score);

            var update = await _scoring.UpdateScore(newscore);

            if (update)
            {
                _repo.Add(newscore);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("summary/{matchId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMatchSummary(int matchId)
        {
            var match = await _repo.GetMatch(matchId);

            if (match == null)
            {
                return BadRequest(new { message = " There is no Such match" });
            }

            IList<Score> Player1Score = new List<Score>();
            IList<Score> Player2Score = new List<Score>();
            IList<Foul> Player1Foul = new List<Foul>();
            IList<Foul> Player2Foul = new List<Foul>();

            var count = 1;
            foreach (var item in match.Players)
            {
                if (count > 1)
                {
                    Player2Score = await _scoring.GetScoresOfMatch(matchId, item.id);
                    Player2Foul = await _scoring.GetFoulOfMatch(matchId, item.id);
                    break;
                }
                Player1Score = await _scoring.GetScoresOfMatch(matchId, item.id);
                Player1Foul = await _scoring.GetFoulOfMatch(matchId, item.id);
                count++;
            }

            return Ok(new
            {
                player1score = Player1Score,
                player1foul = Player1Foul,
                player2score = Player2Score,
                player2foul = Player2Foul
            }
          );
        }

    }
}
