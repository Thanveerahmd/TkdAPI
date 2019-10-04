using System.Threading.Tasks;
using TkdScoringApp.API.Data;
using TkdScoringApp.API.Entities;
using TkdScoringApp.API.Helpers;
using TkdScoringApp.API.iService;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            return await _context.Match.Include(p => p.Players).Include(p => p.Judges).FirstAsync(p => p.Id == id);
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

        public async Task<bool> HasRecord(TempScore score)
        {
            switch (score.Score)
            {
                case 1:
                    {
                        var info = await _context.punch
                        .Where(p => p.MatchId == score.MatchId)
                        .Where(p => p.PlayerId == score.PlayerId)
                        .ToListAsync();

                        if (info.Count != 0)
                            return true;
                        else
                            return false;
                    }

                case 2:
                    {
                        var info = await _context.kickbody
                        .Where(p => p.MatchId == score.MatchId)
                        .Where(p => p.PlayerId == score.PlayerId)
                        .ToListAsync();

                        if (info.Count != 0)
                            return true;
                        else
                            return false;
                    }
                case 3:
                    {
                        var info = await _context.kickhead
                        .Where(p => p.MatchId == score.MatchId)
                        .Where(p => p.PlayerId == score.PlayerId)
                        .ToListAsync();

                        if (info.Count != 0)
                            return true;
                        else
                            return false;
                    }
                case 4:
                    {
                        var info = await _context.turningKickBody
                        .Where(p => p.MatchId == score.MatchId)
                        .Where(p => p.PlayerId == score.PlayerId)
                        .ToListAsync();

                        if (info.Count != 0)
                            return true;
                        else
                            return false;
                    }
                case 5:
                    {
                        var info = await _context.turningKickHead
                        .Where(p => p.MatchId == score.MatchId)
                        .Where(p => p.PlayerId == score.PlayerId)
                        .ToListAsync();

                        if (info.Count != 0)
                            return true;
                        else
                            return false;
                    }
                default:
                    throw new AppException("Score is not Valid");
            }
        }

        public async Task<TempScore> UpdateScore(TempScore score)
        {
            var tempScore = new TempScore();

            switch (score.Score)
            {
                case 1:
                    tempScore = await _context.punch
                  .Where(p => p.MatchId == score.MatchId)
                  .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                  .LastOrDefaultAsync(p => p.PlayerId == score.PlayerId);
                    break;
                case 2:
                    tempScore = await _context.kickbody
                     .Where(p => p.MatchId == score.MatchId)
                     .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                     .LastOrDefaultAsync(p => p.PlayerId == score.PlayerId);
                    break;
                case 3:
                    tempScore = await _context.kickhead
                     .Where(p => p.MatchId == score.MatchId)
                     .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                     .LastOrDefaultAsync(p => p.PlayerId == score.PlayerId);
                    break;
                case 4:
                    tempScore = await _context.turningKickBody
                     .Where(p => p.MatchId == score.MatchId)
                     .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                     .LastOrDefaultAsync(p => p.PlayerId == score.PlayerId);
                    break;
                case 5:
                    tempScore = await _context.turningKickHead
                     .Where(p => p.MatchId == score.MatchId)
                     .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                     .LastOrDefaultAsync(p => p.PlayerId == score.PlayerId);
                    break;
                default:
                    throw new AppException("Score is not Valid");
            }

            var match = await GetMatch(score.MatchId);

            var NoOfJudges = match.NoOfJudges;

            if ((tempScore.NoOfConfirmation + 1) == NoOfJudges)
            {
                return null;
            }

            TimeSpan timeDiff = (score.time - tempScore.time);

            var time = Convert.ToInt32(timeDiff.TotalSeconds);


            if (time < 3)
            {
                switch (score.NoOfConsecutiveTaps)
                {
                    case 1:
                        return await getResult(score, tempScore, NoOfJudges);
                    case 2:
                        return await getResult(score, tempScore, NoOfJudges);
                    case 3:
                        return await getResult(score, tempScore, NoOfJudges);
                    case 4:
                        return await getResult(score, tempScore, NoOfJudges);
                }
            }

            _repo.Delete(tempScore);
            
            if (await _repo.Save())
            {
                return null;
            }

            throw new AppException("Entity is not delete properly");
        }

        public async Task<TempScore> getResult(TempScore score, TempScore tempScore, int NoOfJudges)
        {
            int formula = (NoOfJudges / 2) + 1;

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
                    await DeleteRelevantRecords(tempScore);
                    return null;
                }
                else
                {
                    throw new AppException("Score is not saved");
                }

            }
            else
            {
                score.NoOfConfirmation = tempScore.NoOfConfirmation + 1;
                return score;

            }
        }

        public async Task<bool> DeleteRelevantRecords(TempScore score)
        {

            List<TempScore> tempScore = new List<TempScore>();

            switch (score.Score)
            {
                case 1:
                    var allpunch = await _context.punch
                  .Where(p => p.MatchId == score.MatchId)
                  .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                  .Where(p => p.PlayerId == score.PlayerId)
                  .ToListAsync();

                    _repo.DeleteAll(allpunch);

                    if (await _repo.Save())
                    {
                        return true;
                    }
                    return false;

                //  tempScore = allpunch.Cast<TempScore>().ToList();

                case 2:
                    var allskickbody = await _context.kickbody
                     .Where(p => p.MatchId == score.MatchId)
                     .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                     .Where(p => p.PlayerId == score.PlayerId)
                     .ToListAsync();

                    _repo.DeleteAll(allskickbody);

                    if (await _repo.Save())
                    {
                        return true;
                    }
                    return false;

                case 3:
                    var allkickhead = await _context.kickhead
                      .Where(p => p.MatchId == score.MatchId)
                      .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                      .Where(p => p.PlayerId == score.PlayerId)
                      .ToListAsync();

                    _repo.DeleteAll(allkickhead);

                    if (await _repo.Save())
                    {
                        return true;
                    }
                    return false;
                case 4:
                    var allturningKickBody = await _context.turningKickBody
                     .Where(p => p.MatchId == score.MatchId)
                     .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                      .Where(p => p.PlayerId == score.PlayerId)
                     .ToListAsync();

                    _repo.DeleteAll(allturningKickBody);

                    if (await _repo.Save())
                    {
                        return true;
                    }
                    return false;
                case 5:
                    var allturningKickHead = await _context.turningKickHead
                     .Where(p => p.MatchId == score.MatchId)
                     .Where(p => p.NoOfConsecutiveTaps == score.NoOfConsecutiveTaps)
                      .Where(p => p.PlayerId == score.PlayerId)
                     .ToListAsync();

                    _repo.DeleteAll(allturningKickHead);

                    if (await _repo.Save())
                    {
                        return true;
                    }
                    return false;
                default:
                    throw new AppException("Score is not Valid");


            }
        }
    }
}