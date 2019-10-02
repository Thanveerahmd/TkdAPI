using TkdScoringApp.API.iService;
using System.Collections.Generic;

namespace TkdScoringApp.API.Services
{
    public class TkdRepo : iTkdRepo
    {
        private DataContext _context;

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

    }
}

