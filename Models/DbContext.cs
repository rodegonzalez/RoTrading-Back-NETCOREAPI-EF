using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;

namespace GeneralStore.Models 
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Market> Markets { get; set; } = null!;
        public DbSet<Broker> Brokers { get; set; } = null!;
        public DbSet<Diary> Diaries { get; set; } = null!;
        public DbSet<Divisa> Divisas { get; set; } = null!;
        public DbSet<Position_pattern> Position_patterns { get; set; } = null!;
        public DbSet<Position_highpattern> Position_highpatterns { get; set; } = null!;
        public DbSet<Position_setup> Position_setups { get; set; } = null!;
        public DbSet<Ticker> Tickers { get; set; } = null!;
        public DbSet<Tpp> Tpps { get; set; } = null!;

        public DbSet<PositionView> PositionViews { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PositionView>().HasNoKey().ToView("view_positions");
        }

    }

}