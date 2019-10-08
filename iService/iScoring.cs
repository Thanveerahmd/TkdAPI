using System.Threading.Tasks;
using TkdAPI.Entities;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.iService
{
    public interface iScoring
    {
        Task<bool> HasRecord(TempScore score);
        Task<Match> GetMatch(int id);
        Task<Match> GetMatchByRingId(string ringId);
        Task<bool> UpdateFoul(Foul foul);
        Task<TempScore> UpdateScore(TempScore score);

        Task<bool> RemoveJudge(Judge judge);

    }
}