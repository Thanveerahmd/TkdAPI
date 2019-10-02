using System.Threading.Tasks;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.iService
{
    public interface iScoring
    {
        Task<bool> UpdateScore(TempScore score);
        Task<bool> HasRecord(TempScore score);
        void UpdateFoul(int PlayerId, int foul);
        Task<TempScore> GetTempScore(int id);
        Task<Match> GetMatch(int id);

    }
}