using System.Threading.Tasks;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.iService
{
    public interface iUser
    {
       Task <Admin> GetAdmin(int id);
       Task< Judge> GetJudge(int id);
        Task<Player> GetPlayer(int id);   
    }
}