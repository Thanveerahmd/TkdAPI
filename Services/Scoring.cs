using System.Threading.Tasks;
using TkdScoringApp.API.Data;
using TkdScoringApp.API.Entities;
using TkdScoringApp.API.Helpers;
using TkdScoringApp.API.iService;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using TkdAPI.Entities;
using System.Reflection;

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

        public async Task<bool> HasRecord(TempScore score,string type)
        {

            PropertyInfo prop = typeof(DataContext).GetProperty(type);
            var context = prop.GetValue(_context);

            // var info = await _context.kickhead
            // .Where(p => p.MatchId == score.MatchId)
            // .Where(p => p.PlayerId == score.PlayerId)
            // .ToListAsync();

            // if (info.Count != 0)
            //     return true;
            // else
            //     return false;

            return true;
        }

        public async Task<TempScore> UpdateScore(TempScore score)
        {
            var tempScore = new TempScore();

            switch (score.Score)
            {
                case 1:
                    tempScore = _context.push
                  .Where(p => p.MatchId == score.MatchId)
                  .LastOrDefault(p => p.PlayerId == score.PlayerId);
                    break;
                case 2:
                    tempScore = _context.kickbody
                     .Where(p => p.MatchId == score.MatchId)
                     .LastOrDefault(p => p.PlayerId == score.PlayerId);
                    break;
                case 3:
                    tempScore = _context.kickhead
                     .Where(p => p.MatchId == score.MatchId)
                     .LastOrDefault(p => p.PlayerId == score.PlayerId);
                    break;
                case 4:
                    tempScore = _context.turningKickBody
                     .Where(p => p.MatchId == score.MatchId)
                     .LastOrDefault(p => p.PlayerId == score.PlayerId);
                    break;
                case 5:
                    tempScore = _context.turningKickHead
                     .Where(p => p.MatchId == score.MatchId)
                     .LastOrDefault(p => p.PlayerId == score.PlayerId);
                    break;
                default:
                    throw new AppException("Score is not Valid");
            }
    
            var match = await GetMatch(score.MatchId);

            if ((tempScore.NoOfConfirmation + 1) == match.NoOfJudges)
            {
                return null;
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
                        //add signalR

                        score.NoOfConfirmation = tempScore.NoOfConfirmation + 1;
                        var newscore = new Score();

                        newscore.MatchId = tempScore.MatchId;
                        newscore.PlayerId = tempScore.PlayerId;
                        newscore.ScoreValue = score.Score;

                        _repo.Add(newscore);

                        var player = await _user.GetPlayer(score.PlayerId);
                        player.Totalscore += score.Score;
                        _context.Player.Update(player);

                        if (await _repo.Save())
                        {
                            return score;
                        }
                        else
                        {
                            throw new AppException("Score is not saved");
                        }

                    }
                }
                return null;
            }
            return null;
        }

       
    }
}