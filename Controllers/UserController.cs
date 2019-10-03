using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TkdScoringApp.API.Dto;
using TkdScoringApp.API.Entities;
using TkdScoringApp.API.iService;

namespace TkdScoringApp.API.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly iTkdRepo _repo;
        private readonly  iScoring _score;

        public UserController(
             IMapper mapper,
             iTkdRepo repo,
             iScoring score
             )
        {
            _repo = repo;
            _mapper = mapper;
            _score = score;
        }

        [HttpPost("admin")]
        [AllowAnonymous]
        public async Task<IActionResult> AddAdmin(AdminDto admin)
        {
            var adminUser = _mapper.Map<Admin>(admin);
            _repo.Add(adminUser);

            if (await _repo.Save())
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("judge")]
        [AllowAnonymous]
        public  async Task<IActionResult> AddJudge(JudgeDto judge)
        {
            var judgeUser = _mapper.Map<Judge>(judge);

            var match  =await _score.GetMatch(judgeUser.MatchId);

            if (match == null)
            {
                return BadRequest(new { message = "There is No such match" });

            }


            match.Judges.Add(judgeUser);

            _repo.Add(judgeUser);

            if (await _repo.Save())
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("player")]
        [AllowAnonymous]
        public  async Task<IActionResult> AddPlayer(PlayerDto player)
        {
            var playerUser = _mapper.Map<Player>(player);

            var match  =await _score.GetMatch(playerUser.MatchId);

            if (match == null)
            {
                return BadRequest(new { message = "There is No such match" });

            }
            
            match.Players.Add(playerUser);
            
            _repo.Add(playerUser);

            if (await _repo.Save())
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}