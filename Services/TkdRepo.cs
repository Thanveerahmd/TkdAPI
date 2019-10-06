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
    public class TkdRepo : iTkdRepo
    {
        private readonly DataContext _context;

        public TkdRepo(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }


        public void DeleteAll<T>(ICollection<T> entity) where T : class
        {
            _context.RemoveRange(entity);

        }

        public void AddAll<T>(ICollection<T> entity) where T : class
        {
            _context.AddRange(entity);

        }
    
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Match> checkWhetherRingAvailable(string ring)
        {
            return await _context.Match.FirstOrDefaultAsync(p => p.RingId == ring && p.isFinished == false);
        }
    }
}