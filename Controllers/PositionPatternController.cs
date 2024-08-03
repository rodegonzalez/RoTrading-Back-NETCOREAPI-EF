using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class PositionPatternController
    {
        public static void MapEndpoints_PositionPatterns(this WebApplication app)
        {
            // Paterns ---------------------------------------------------------------------------------------------------------
            app.MapGet("/api/position_patterns", async (Db db) => await db.Position_patterns
                                                                .Where(a => a.Deleted == 0 && a.Active == 1)
                                                                .ToListAsync());

            // High Patterns ----------------------------------------------------------------------------------------------------
            app.MapGet("/api/position_highpatterns", async (Db db) => await db.Position_highpatterns
                                                                .Where(a => a.Deleted == 0 && a.Active == 1)
                                                                .ToListAsync());


            app.MapGet("/api/position_highpattern/{id}", async (Db db, int id) => await db.Position_highpatterns
                                                                  .Where(a => a.Id == id && a.Deleted == 0)
                                                                  //.FirstOrDefaultAsync());    
                                                                  .Take(1)
                                                                  .ToListAsync());

            app.MapPost("/api/position_highpattern", async (Db db, Position_highpattern record) =>
            {
                record.Creation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Active = 1;
                record.Deleted = 0;
                await db.Position_highpatterns.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/position_highpattern/{id}", async (Db db, Position_highpattern updaterecord, int id) =>
            {
                var record = await db.Position_highpatterns
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (record is null) return Results.NotFound();

                record.Name = updaterecord.Name;
                record.Description = updaterecord.Description;
                record.Status = updaterecord.Status;
                record.Active = updaterecord.Active;
                record.Note = updaterecord.Note;

                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/position_highpattern/{id}", async (Db db, int id) =>
            {
                var record = await db.Position_highpatterns
                                    .Where(a => a.Id == id && a.Deleted == 0)
                                    .FirstOrDefaultAsync();
                if (record is null)
                {
                    return Results.NotFound();
                }
                record.Deleted = 1;
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Name = $"{record.Name}_[{record.Id}_{record.Modification}]_Deleted";
                await db.SaveChangesAsync();
                return Results.Ok();
            });



        }

    }


}