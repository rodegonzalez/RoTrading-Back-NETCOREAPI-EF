using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Models 
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Broker> Brokers { get; set; } = null!;
        public DbSet<Diary> Diaries { get; set; } = null!;
        public DbSet<Divisa> Divisas { get; set; } = null!;
        public DbSet<Positions_pattern> Positions_patterns { get; set; } = null!;
        public DbSet<Position_setup> Position_setups { get; set; } = null!;
        public DbSet<Ticker> Tickers { get; set; } = null!;
        public DbSet<Tpp> Tpps { get; set; } = null!;
    }
}