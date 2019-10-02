using System.Threading.Tasks;
using TkdScoringApp.API.Data;
using TkdScoringApp.API.Entities;
using TkdScoringApp.API.iService;

namespace TkdScoringApp.API.Services
{
    public class UserService : iUser
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        public Task<Admin> GetAdmin(int id)
        {
            return _context.Admin.FindAsync(id);
        }

        public Task<Judge> GetJudge(int id)
        {
            return _context.Judge.FindAsync(id);
        }

        public Task<Player> GetPlayer(int id)
        {
            return _context.Player.FindAsync(id);
        }
    }
}