using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using  GeneralStore.Repositories;

namespace GeneralStore.Controllers
{
    public static class TickerController
    {
        public static void MapEndpoints_Tickers(this WebApplication app)
        {
            app.MapGet("/api/tickers", async (Db db) => await db.Tickers
                                                                .Where(a => a.Deleted == 0)
                                                                .ToListAsync());

            app.MapGet("/api/ticker/{id}", async (Db db, int id) => await db.Tickers
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                          //.FirstOrDefaultAsync());    
                                                                          .Take(1)
                                                                          .ToListAsync());

            app.MapPost("/api/ticker", async (Db db, Ticker record) =>
            {
                record.Creation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Active = 1;
                record.Deleted = 0;
                await db.Tickers.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/ticker/{id}", async (Db db, Ticker updaterecord, int id) =>
            {
                var record = await db.Tickers
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (record is null) return Results.NotFound();

                record.Name = updaterecord.Name;
                record.Description = updaterecord.Description;
                record.Status = updaterecord.Status;
                record.Active = updaterecord.Active;
                record.Note = updaterecord.Note;
                record.Modification = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/ticker/{id}", async (Db db, int id) =>
            {
                var record = await db.Tickers
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