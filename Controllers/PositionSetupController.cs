using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class PositionSetupController
    {
        public static void MapEndpoints_PositionSetups(this WebApplication app)
        {
            app.MapGet("/api/position_setups", async (Db db) => await db.Position_setups
                                                                .Where(a => a.Deleted == 0 && a.Active == 1)
                                                                .ToListAsync());

            app.MapGet("/api/position_setup/{id}", async (Db db, int id) => await db.Position_setups
                                                                  .Where(a => a.Id == id && a.Deleted == 0)
                                                                  //.FirstOrDefaultAsync());    
                                                                  .Take(1)
                                                                  .ToListAsync());

            app.MapPost("/api/position_setup", async (Db db, Position_setup record) =>
            {
                record.Creation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Active = 1;
                record.Deleted = 0;
                await db.Position_setups.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/position_setup/{id}", async (Db db, Position_setup updaterecord, int id) =>
            {
                var record = await db.Position_setups
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
            app.MapDelete("/api/position_setup/{id}", async (Db db, int id) =>
            {
                var record = await db.Position_setups
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