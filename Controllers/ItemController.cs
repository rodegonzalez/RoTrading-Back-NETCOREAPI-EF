using Microsoft.EntityFrameworkCore;
using ItemsStore.Models;

namespace ItemsStore.Controllers
{
    public static class ItemsController
    {
        public static void MapItemsEndpoints(this WebApplication app)
        {
            app.MapGet("/api/items", async (Db db) => await db.Items.ToListAsync());
            app.MapGet("/api/item/{id}", async (Db db, int id) => await db.Items.FindAsync(id));    

            app.MapPost("/api/item", async (Db db, Item item) =>
            {
                await db.Items.AddAsync(item);
                await db.SaveChangesAsync();
                return Results.Created($"/item/{item.Id}", item);
            });
            app.MapPut("/api/item/{id}", async (Db db, Item updateitem, int id) =>
            {
                var item = await db.Items.FindAsync(id);
                if (item is null) return Results.NotFound();
                item.ItemName = updateitem.ItemName;
                item.ItemValue = updateitem.ItemValue;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/item/{id}", async (Db db, int id) =>
            {
            var item = await db.Items.FindAsync(id);
            if (item is null)
            {
                return Results.NotFound();
            }
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return Results.Ok();
            });
        }
    }
}