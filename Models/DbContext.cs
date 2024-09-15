using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Models 
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Market> Markets { get; set; } = null!;
        public DbSet<Diary> Diaries { get; set; } = null!;
        public DbSet<Divisa> Divisas { get; set; } = null!;
        public DbSet<Position_pattern> Position_patterns { get; set; } = null!;
        public DbSet<Position_highpattern> Position_highpatterns { get; set; } = null!;
        public DbSet<Position_setup> Position_setups { get; set; } = null!;
        public DbSet<Ticker> Tickers { get; set; } = null!;
        public DbSet<Tickeraccount> Tickeraccounts { get; set; } = null!;
        public DbSet<Tpp> Tpps { get; set; } = null!;
        public DbSet<Tppblock> TppBlocks { get; set; }
        public DbSet<Tppblocksecuence> TppBlockSecuences { get; set; }
        public DbSet<Session> Sessions { get; set; } = null!;

        public DbSet<PositionView> PositionViews { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Views
            modelBuilder.Entity<PositionView>().HasNoKey().ToView("view_positions");

            // MM tables with complex primary keys
            modelBuilder.Entity<Tickeraccount>().HasKey(ta => new { ta.Tickerid, ta.Accountid });
        }

    }

}