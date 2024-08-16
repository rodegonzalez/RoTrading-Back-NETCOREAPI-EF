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

                record.Sessionid = updaterecord.Sessionid;
                record.Guid = updaterecord.Guid;
                record.Tppid = updaterecord.Tppid;
                record.Tppcheck = updaterecord.Tppcheck;
                record.Tppblocksec = updaterecord.Tppblocksec;
                record.Sec = updaterecord.Sec;
                //record.Creation = updaterecord.Creation;
                record.Timein = updaterecord.Timein;
                record.Timeout = updaterecord.Timeout;
                record.Pricein = updaterecord.Pricein;
                record.Priceout = updaterecord.Priceout;
                record.Buysell = updaterecord.Buysell;
                record.Contracts = updaterecord.Contracts;
                record.Opresultticks = updaterecord.Opresultticks;
                record.Usdeur = updaterecord.Usdeur;
                record.Opresult = updaterecord.Opresult;
                record.Commission = updaterecord.Commission;
                record.Opresulteur = updaterecord.Opresulteur;
                record.Divisaid = updaterecord.Divisaid;
                record.Accountid = updaterecord.Accountid;
                record.Tickerid = updaterecord.Tickerid;
                record.Pattern1id = updaterecord.Pattern1id;
                record.Pattern2id = updaterecord.Pattern2id;
                record.Setup1id = updaterecord.Setup1id;
                record.Setup2id = updaterecord.Setup2id;
                //record.Processed = updaterecord.Processed;
                //record.Deleted = updaterecord.Deleted;
                //record.Deletednote = updaterecord.Deletednote;
                record.Imagepath = updaterecord.Imagepath;
                record.Note = updaterecord.Note;
                record.Status = updaterecord.Status;

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