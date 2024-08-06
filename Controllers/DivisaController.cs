using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using System.Diagnostics.Eventing.Reader;

namespace GeneralStore.Controllers
{
    public static class DivisaController
    {
        public static void MapEndpoints_Divisas(this WebApplication app)
        {
            app.MapGet("/api/divisas", async (Db db) => await db.Divisas
                                                                .Where(a => a.Deleted == 0)
                                                                .Take(100)
                                                                .ToListAsync());            

            app.MapGet("/api/divisa/{id}", async (Db db, int id) => await db.Divisas
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                            //.FirstOrDefaultAsync());    
                                                                            .Take(1)
                                                                            .ToListAsync());

            app.MapPost("/api/divisa", async (Db db, Divisa record) =>
            {
                record.Creation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Active = 1;
                record.Deleted = 0;
                await db.Divisas.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/divisa/{id}", async (Db db, Tpp updaterecord, int id) =>
            {
                var record = await db.Divisas
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (record is null) return Results.NotFound();

                record.Name = updaterecord.Name;
                record.Description = updaterecord.Description;  
                record.Active = updaterecord.Active;
                record.Note = updaterecord.Note;

                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/divisa/{id}", async (Db db, int id) =>
            {
                var record = await db.Divisas
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