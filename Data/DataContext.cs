using Microsoft.EntityFrameworkCore;
using TkdScoringApp.API.Entities;

namespace TkdScoringApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Judge> Judge { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<TempScore> TempScore { get; set; }
        
    }
}
