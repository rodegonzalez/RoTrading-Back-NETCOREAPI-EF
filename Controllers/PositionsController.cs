using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using  GeneralStore.Repositories;

namespace GeneralStore.Controllers
{
    public static class PositionController
    {
        public static void MapEndpoints_Positions(this WebApplication app)
        {
            app.MapGet("/api/positions", async (Db db) => await db.Positions
                                                                .Where(a => a.Deleted == 0)
                                                                .ToListAsync());

            app.MapGet("/api/position/{id}", async (Db db, int id) => await db.Positions
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                            .FirstOrDefaultAsync());    

            app.MapPost("/api/position", async (Db db, Position record) =>
            {
                await db.Positions.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/position/{id}", async (Db db, Position updaterecord, int id) =>
            {
                var record = await db.Positions
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (record is null) return Results.NotFound();

                //record.Modification = "xx-xx-xxxx";
                
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/position/{id}", async (Db db, int id) =>
            {
                var record = await db.Positions
                                    .Where(a => a.Id == id && a.Deleted == 0)
                                    .FirstOrDefaultAsync();
                if (record is null)
                {
                    return Results.NotFound();
                }
                //db.Items.Remove(item);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}