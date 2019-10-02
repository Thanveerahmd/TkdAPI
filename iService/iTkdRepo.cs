using System.Collections.Generic;
using System.Threading.Tasks;

namespace TkdScoringApp.API.iService
{
    public interface iTkdRepo
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void AddAll<T>(ICollection<T> entity) where T : class;
        void DeleteAll<T>(ICollection<T> entity) where T : class;
        Task<bool> Save();
    }
}