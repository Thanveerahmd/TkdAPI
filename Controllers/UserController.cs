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

        public UserController(
             IMapper mapper,
             iTkdRepo repo
             )
        {
            _repo = repo;
            _mapper = mapper;
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
            
            _repo.Add(playerUser);

            if (await _repo.Save())
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}