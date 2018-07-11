using BetSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BetSystem.Persistence
{
    public class BetDbContext: DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Season> Seasons { get; set; }

        public BetDbContext(DbContextOptions<BetDbContext> options): base(options)
        {
            
        }
    }
}