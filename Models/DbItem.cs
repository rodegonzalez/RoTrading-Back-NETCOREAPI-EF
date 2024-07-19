using Microsoft.EntityFrameworkCore;

namespace ItemsStore.Models 
{
    public class Item
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? ItemValue { get; set; }
    }

    class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set; } = null!;
    }
}