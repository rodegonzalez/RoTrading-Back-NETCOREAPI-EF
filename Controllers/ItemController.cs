using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using  GeneralStore.Repositories;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class ItemsController
    {
        public static void MapItemsEndpoints(this WebApplication app)
        {
            app.MapGet("/api/items", async (Db db) => await db.Items.ToListAsync());
            // app.MapGet("/api/items", async () => await GetAllAsync_Items());

            app.MapGet("/api/item/{id}", async (Db db, int id) => await db.Items.FindAsync(id));    

            app.MapPost("/api/item", async (Db db, Item record) =>
            {
                await db.Items.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/item/{id}", async (Db db, Item updaterecord, int id) =>
            {
                var record = await db.Items.FindAsync(id);
                if (record is null) return Results.NotFound();
                record.ItemName = updaterecord.ItemName;
                record.ItemValue = updaterecord.ItemValue;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/item/{id}", async (Db db, int id) =>
            {
                var record = await db.Items.FindAsync(id);
                if (record is null)
                {
                    return Results.NotFound();
                }
                db.Items.Remove(record);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}