using Microsoft.EntityFrameworkCore;
using TkdAPI.Entities;
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
        public DbSet<Score> Score { get; set; }
        public DbSet<Foul> Foul { get; set; }
        public DbSet<Punch> punch { get; set; }
        public DbSet<KickBody> kickbody { get; set; }
        public DbSet<Kickhead> kickhead { get; set; }
        public DbSet<TurningKickBody> turningKickBody { get; set; }
        public DbSet<TurningKickHead> turningKickHead { get; set; }
    }
}
