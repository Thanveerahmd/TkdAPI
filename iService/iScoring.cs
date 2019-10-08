using System.Collections.Generic;
using System.Threading.Tasks;
using TkdAPI.Entities;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.iService
{
    public interface iScoring
    {
        Task<bool> HasRecord(TempScore score);
        Task<Match> GetMatch(int id);
        Task<IList<Score>> GetScoresOfMatch(int matchId, int playerId);
        Task<IList<Score>> GetScore(int matchId);
        Task<IList<Foul>> GetFoul(int matchId);
        Task<IList<Foul>> GetFoulOfMatch(int matchId, int playerId);
        Task<Match> GetMatchByRingId(string ringId);
        Task<bool> UpdateFoul(Foul foul);
        Task<TempScore> UpdateScore(TempScore score);
        Task<bool> UpdateScoreManual(Score score);
        Task<bool> RemoveJudge(Judge judge);

    }
}