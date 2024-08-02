using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using System.Diagnostics.Eventing.Reader;

namespace GeneralStore.Controllers
{
    public static class TppController
    {
        public static void MapEndpoints_Tpps(this WebApplication app)
        {
            app.MapGet("/api/tpps", async (Db db) => await db.Tpps
                                                                .Where(a => a.Deleted == 0)
                                                                .Take(100)
                                                                .ToListAsync());            

            app.MapGet("/api/tpp/{id}", async (Db db, int id) => await db.Tpps
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                            //.FirstOrDefaultAsync());    
                                                                            .Take(1)
                                                                            .ToListAsync());

            app.MapPost("/api/tpp", async (Db db, Tpp record) =>
            {
                record.Creation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Active = 1;
                record.Deleted = 0;
                await db.Tpps.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/tpp/{id}", async (Db db, Tpp updaterecord, int id) =>
            {
                var record = await db.Tpps
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (record is null) return Results.NotFound();

                record.Name = updaterecord.Name;
                record.Description = updaterecord.Description;  
                record.Status = updaterecord.Status;
                //record.Active = updaterecord.Active;
                record.Active = 1;
                record.Note = updaterecord.Note;

                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/tpp/{id}", async (Db db, int id) =>
            {
                var record = await db.Tpps
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