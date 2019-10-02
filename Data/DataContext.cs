using Microsoft.EntityFrameworkCore;
using TkdScoringApp.API.Entity;

namespace TkdScoringApp.API.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
     

    }
}


