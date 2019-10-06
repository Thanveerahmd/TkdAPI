using System.Threading.Tasks;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.iService
{
    public interface iUser
    {
       Task <Admin> GetAdmin(int id);

        Task<Admin> GetAdminByUsername(string username);
       Task< Judge> GetJudge(int id);
        Task<Player> GetPlayer(int id);   
    }
}