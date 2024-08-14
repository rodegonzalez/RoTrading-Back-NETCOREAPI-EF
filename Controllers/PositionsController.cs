using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using System.Diagnostics.CodeAnalysis;

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
                string _now = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");

                record.Modification = _now;
                await db.Positions.AddAsync(record);
                await db.SaveChangesAsync();

                // create tppblock if not exists
                Tppblock? tppblock = await db.TppBlocks
                                            .Where(a => a.Tppid == record.Tppid && a.Tppblocksec == record.Tppblocksec)
                                            .FirstOrDefaultAsync();

                if (tppblock is null)
                {
                    Tppblock newtppblock = new Tppblock();
                    newtppblock.Tppid = record.Tppid;
                    newtppblock.Tppblocksec = record.Tppblocksec;
                    newtppblock.Creation = _now;
                    newtppblock.Modification = _now;
;                   await db.TppBlocks.AddAsync(newtppblock);
                }

                // update block secuence
                Tppblocksecuence tppblocksecuence = new Tppblocksecuence();
                tppblocksecuence.Positionid = record.Id;
                tppblocksecuence.Tppid = record.Tppid;
                tppblocksecuence.Tppblocksec = record.Tppblocksec;
                tppblocksecuence.Sec = record.Sec;
                tppblocksecuence.Creation = _now;
                tppblocksecuence.Modification = _now;
                await db.TppBlockSecuences.AddAsync(tppblocksecuence);

                // update db
                await db.SaveChangesAsync();

                // return post 
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