using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class PositionController { 
        public static void MapEndpoints_Positions(this WebApplication app)
        {
            app.MapGet("/api/positions", async (Db db) => await db.PositionViews
                                                                .Where(a => a.Deleted == 0)
                                                                .Take(100)
                                                                .ToListAsync());
            app.MapGet("/api/positions/opened", async (Db db) => await db.PositionViews
                                                                .Where(a => a.Deleted == 0 && a.Status == "opened")
                                                                .ToListAsync());
            app.MapGet("/api/positions/notopened", async (Db db) => await db.PositionViews
                                                                .Where(a => a.Deleted == 0 && a.Status != "opened")
                                                                .Take(10)
                                                                .ToListAsync());

            app.MapGet("/api/position/{id}", async (Db db, int id) => await db.PositionViews
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                            .FirstOrDefaultAsync());

            app.MapPost("/api/position", async (Db db, Position record) =>
            {
                Console.WriteLine("Record: " + record);

                record.Modification = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
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

                record.Modification = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
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
                record.Deleted = 1;
                record.Modification = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
}
}