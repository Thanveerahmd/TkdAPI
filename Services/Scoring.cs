using System.Threading.Tasks;
using TkdScoringApp.API.Data;
using TkdScoringApp.API.Entities;
using TkdScoringApp.API.Helpers;
using TkdScoringApp.API.iService;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace TkdScoringApp.API.Services
{
    public class Scoring : iScoring
    {
        private readonly DataContext _context;
        private readonly iTkdRepo _repo;
        private readonly iUser _user;

        public Scoring(DataContext context, iTkdRepo repo, iUser user)
        {
            _context = context;
            _repo = repo;
            _user = user;
        }
        public async Task<TempScore> GetTempScore(int id)
        {
            return await _context.TempScore.FindAsync(id);
        }

        public async Task<Match> GetMatch(int id)
        {
            return await _context.Match.FindAsync(id);
        }

        public async void UpdateFoul(int PlayerId, int foul)
        {
            var user = await _user.GetPlayer(PlayerId);

            if (user == null)
            {
                throw new AppException("There is no Such Player");
            }
            user.Totalfoul += foul;

            _context.Player.Update(user);
        }


        public async Task<bool> UpdateScore(TempScore score)
        {
            var tempScore = _context.TempScore
            .Where(p => p.MatchId == score.MatchId)
            .LastOrDefault(p => p.PlayerId == score.PlayerId);

            var match = await GetMatch(score.MatchId);

            if ((tempScore.NoOfConfirmation + 1) == match.NoOfJudges)
            {
                return true;
            }

            TimeSpan timeDiff = (score.time - tempScore.time);

            var time = Convert.ToInt32(timeDiff.TotalSeconds);

            int formula = (match.NoOfJudges / 2) + 1;

            if (time < 3)
            {
                if (tempScore.JudgeId != score.JudgeId)
                {
                    if ((tempScore.NoOfConfirmation + 1) == formula)
                    {
                       //add singnalR
                       //ADD Score entity

                    }
                }
                return true;
            }
            return true;
        }

        public async Task<bool> HasRecord(TempScore score)
        {

            var info = await _context.TempScore
            .Where(p => p.MatchId == score.MatchId)
            .Where(p => p.PlayerId == score.PlayerId)
            .ToListAsync();

            if (info.Count != 0)
                return true;
            else
                return false;
        }

    }
}