using System.Threading.Tasks;
using TkdAPI.Entities;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.iService
{
    public interface iScoring
    {
        Task<bool> HasRecord(TempScore score);
        void UpdateFoul(int PlayerId, int foul);
        Task<Match> GetMatch(int id);

        Task<Match> GetMatchByRingId(string ringId);

        Task<TempScore> UpdateScore(TempScore score);
    }
}