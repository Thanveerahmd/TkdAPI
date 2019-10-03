using System.Threading.Tasks;
using TkdAPI.Entities;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.iService
{
    public interface iScoring
    {
        Task<bool> HasRecord(TempScore score,string type);
        void UpdateFoul(int PlayerId, int foul);
        Task<Match> GetMatch(int id);
        Task<TempScore> UpdateScore(TempScore score);
    }
}