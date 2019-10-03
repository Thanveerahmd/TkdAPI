using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TkdAPI.Entities;
using TkdScoringApp.API.Dto;
using TkdScoringApp.API.Entities;
using TkdScoringApp.API.iService;

namespace TkdScoringApp.API.Controllers
{
    [ApiController]
    [Route("match")]
    public class MatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly iTkdRepo _repo;
        private readonly iScoring _scoring;

        public MatchController(
               IMapper mapper,
               iTkdRepo repo,
               iScoring scoring
               )
        {
            _repo = repo;
            _scoring = scoring;
            _mapper = mapper;

        }

        [HttpPost("match")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMatch(MatchDto match)
        {
            var newMatch = _mapper.Map<Match>(match);
            _repo.Add(newMatch);
            if (await _repo.Save())
            {
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
                    return Ok();
                }
                return BadRequest();
                // if (await _scoring.UpdateScore(newscore))
                // {
                //     _repo.Add(newscore);

                //     if (await _repo.Save())
                //     {
                //         return Ok();
                //     }
                //     return BadRequest();
                // }

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



    }
}