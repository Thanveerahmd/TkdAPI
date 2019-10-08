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
    public class UserService : iUser
    {
        private readonly DataContext _context;
        private readonly iTkdRepo _repo;

        public UserService(DataContext context, iTkdRepo repo)
        {
            _context = context;
            _repo = repo;
        }
        public Task<Admin> GetAdmin(int id)
        {
            return _context.Admin.FindAsync(id);
        }

        public async Task<Admin> GetAdminByUsername(string username)
        {
            var data = await _context.Admin.FirstOrDefaultAsync(p => p.Username == username);
            return data;
        }

        public async Task<Judge> GetJudge(int id)
        {
            return await _context.Judge.FindAsync(id);
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _context.Player.FindAsync(id);
        }

        
    }
}