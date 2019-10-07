using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TkdScoringApp.API.Dto;
using TkdScoringApp.API.Entities;
using TkdScoringApp.API.iService;
using Microsoft.AspNetCore.SignalR;
using TkdScoringApp.API.Helpers;

namespace TkdScoringApp.API.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly iTkdRepo _repo;
        private readonly iScoring _score;

        private readonly iUser _user;

        private IHubContext<ChartHub> _hub;

        public UserController(
             IMapper mapper,
             iTkdRepo repo,
             iScoring score,
             IHubContext<ChartHub> hub,
             iUser user
             )
        {
            _repo = repo;
            _mapper = mapper;
            _score = score;
            _hub = hub;
            _user = user;
        }

        [HttpPost("admin/signup")]
        [AllowAnonymous]
        public async Task<IActionResult> AddAdmin(AdminDto admin)
        {
            var adminUser = _mapper.Map<Admin>(admin);

            var adminDatabaseRecord = await _user.GetAdminByUsername(adminUser.Username);
            if (adminDatabaseRecord == null)
            {
                _repo.Add(adminUser);

                if (await _repo.Save())
                {
                    return await AdminLogin(admin);
                }
                return BadRequest();
            }

            return BadRequest(new { message = "Username not found" });
        }

        [HttpGet("admin/signup/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckAdminUsername(string username)
        {

            var adminDatabaseRecord = await _user.GetAdminByUsername(username);
            if (adminDatabaseRecord == null)
            {
                return Ok(new { recordFound = false });
            }
            return Ok(new { recordFound = true });

        }

        [HttpPost("admin/login")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminLogin(AdminDto admin)
        {
            var adminUser = _mapper.Map<Admin>(admin);

            var adminDatabaseRecord = await _user.GetAdminByUsername(adminUser.Username);

            if (adminDatabaseRecord == null)
            {
                return BadRequest(new { message = "Admin Username not found" });
            }

            var token = Extension.OTPCharacters("10");
            var dataReturn = _mapper.Map<AdminTokenReturnDto>(adminDatabaseRecord);
            dataReturn.Token = token;
            return Ok(dataReturn);

        }

        [HttpPost("judge")]
        [AllowAnonymous]
        public async Task<IActionResult> AddJudge(JudgeDto judge)
        {
            var judgeUser = _mapper.Map<Judge>(judge);
            var match = new Match();
            if (judgeUser.MatchId != 0)
            {
                match = await _score.GetMatch(judgeUser.MatchId);
            }
            else
            {
                if (judge.RingId != null)
                {
                    match = await _score.GetMatchByRingId(judge.RingId);
                    if (match == null)
                    {
                        return BadRequest(new { message = $"No Match Happening in Ring {judge.RingId}" });
                    }
                    judgeUser.MatchId = match.Id;
                }
                else
                {
                    return BadRequest();
                }
            }

            if (match == null)
            {
                return BadRequest(new { message = "There is No such match" });

            }

            if (match.Judges.Count + 1 > match.NoOfJudges)
            {
                return BadRequest(new { message = "Already judge limit have been allocated for this match" });
            }


            _repo.Add(judgeUser);
            match.Judges.Add(judgeUser);

            if (await _repo.Save())
            {
                await _hub.Clients.All.SendAsync("judgeJoinUpdate", judgeUser);
                return Ok(match);
            }

            return BadRequest();
        }

        [HttpPost("player")]
        [AllowAnonymous]
        public async Task<IActionResult> AddPlayer(PlayerDto player)
        {
            var playerUser = _mapper.Map<Player>(player);

            var match = await _score.GetMatch(playerUser.MatchId);

            if (match == null)
            {
                return BadRequest(new { message = "There is No such match" });

            }

            if (match.Players.Count + 1 > 2)
            {
                return BadRequest(new { message = "Already 2 players have been allocated for this match" });
            }

            _repo.Add(playerUser);

            match.Players.Add(playerUser);


            if (await _repo.Save())
            {
                return Ok(playerUser);
            }

            return BadRequest();
        }

    }
}