using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

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
                                                                            .FirstOrDefaultAsync());

            app.MapGet("/api/tpp/getsecuence/{id}", async (Db db, int id) =>
            {
                // Obtener el valor máximo de tppblock para el tppid dado
                var maxTppBlock = db.TppBlocks
                                          .Where(tb => tb.Tppid == id)
                                          .Max(tb => (int?)tb.Tppblocksec);

                if (maxTppBlock == null)
                {
                    return Results.NotFound("No se encontró ningún tppblock para el tppid proporcionado.");
                }

                // Obtener el valor máximo de tppblocksecuence para el tppblock máximo
                var maxTppBlockSecuence = db.TppBlockSecuences
                                                  .Where(tbs => tbs.Tppid == id && tbs.Tppblocksec == maxTppBlock)
                                                  .Max(tbs => (int?)tbs.Sec);

                return Results.Ok(new
                {
                    Tppid = id,
                    Tppblocksec = maxTppBlock,
                    Sec = maxTppBlockSecuence
                });
            });

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
                record.Active = updaterecord.Active;
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