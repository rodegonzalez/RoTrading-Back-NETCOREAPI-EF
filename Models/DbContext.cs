using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Models 
{
    class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; } = null!;
    }
}