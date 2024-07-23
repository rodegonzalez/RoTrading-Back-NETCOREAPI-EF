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
        public DbSet<Broker> Brokers { get; set; } = null!;
        public DbSet<Diary> Diaries { get; set; } = null!;
        public DbSet<Divisa> Divisas { get; set; } = null!;
        public DbSet<Positions_pattern> Positions_patterns { get; set; } = null!;
        public DbSet<Position_setup> Position_setups { get; set; } = null!;
        public DbSet<Ticker> Tickers { get; set; } = null!;
        public DbSet<Tpp> Tpps { get; set; } = null!;

        public DbSet<PositionView> PositionViews { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PositionView>().HasNoKey().ToView("view_positions");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                          .EnableSensitiveDataLogging()
                          .UseSqlite("Data Source=rotrading.db");
        }

    }

    public class MiInterceptorDeConsultas : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            Console.WriteLine("###########");
            Console.WriteLine($"##############  Consulta SQL: {command.CommandText}");
            Console.WriteLine("###########");

            return base.ReaderExecuting(command, eventData, result);
        }
    }

}