using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using System.Linq;
using BK_NetAPI_SQLite.Interfaces;
using BK_NetAPI_SQLite.Services;

namespace GeneralStore.Controllers
{
    public static class SessionController
    {
        public static void MapEndpoints_Sessions(this WebApplication app)
        {
            /*
            app.MapGet("/api/sessions", async (IPositionsSession repo) => await repo.GetAllAsync());
            app.MapGet("/api/session/{id}", async (IPositionsSession repo, string id) => await repo.GetByIdAsync(id));
            app.MapGet("/api/session/last", async (IPositionsSession repo) => await repo.GetLastAsync());

            app.MapPost("/api/session", async (PositionsSessionService service, string id) =>
            {
                var newItem = await service.CreateItemAsync(id);
                return Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/sessions/{id}", async (PositionsSessionService service, Session updaterecord, string id) =>
            {
                var updatedItem = await service.UpdateItemAsync(updaterecord, id);
                if (updatedItem is null) return Results.NotFound();
                return Results.NoContent();
            });

            app.MapDelete("/api/position/{id}", async (PositionsSessionService service, string id) =>
            {
                var item = await service.DeleteItemAsync(id);
                if (item is null) return Results.NotFound();
                return Results.Ok();
            });
            */


            /* ------------------------------------------------- */
            /* ------------------------------------------------- */

            app.MapGet("/api/sessions", async (Db db) => await db.Sessions.ToListAsync());                                                                

            app.MapGet("/api/session/{id}", async (Db db, string id) => await db.Sessions
                                                                           .Where(a => a.Id == id)
                                                                          .FirstOrDefaultAsync());    
                                                                         
            app.MapGet("/api/session/last", async (Db db) => await db.Sessions
                                                             .OrderByDescending(a => a.Id)
                                                             .FirstOrDefaultAsync());

            app.MapPost("/api/session/createSessionIfNotExists/{id}", async (Db db, string id) =>
            {
                // Obtener una session si existe
                var session = await db.Sessions
                                .Where(tb => tb.Id == id)
                                .FirstOrDefaultAsync();

                if (session == null)
                { 
                    var newSession = new Session();
                    newSession.Id = id;
                    newSession.Creation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    newSession.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    newSession.Usdeur = 0;
                    newSession.Haspositions = 0;
                    newSession.Consolidated = 0;
                    newSession.Sessionnote = "";
                    newSession.Active = 1;
                    newSession.Deleted = 0;
                    await db.Sessions.AddAsync(newSession);
                    await db.SaveChangesAsync();
                    return Results.Created($"/record/{newSession.Id}", newSession);
                }
                return Results.Ok(session);
            });

            app.MapPut("/api/session/{id}", async (Db db, Session updaterecord, string id) =>
            {
                var record = await db.Sessions
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (record is null) return Results.NotFound();

                record.Usdeur = updaterecord.Usdeur;
                record.Haspositions = updaterecord.Haspositions;
                record.Consolidated = updaterecord.Consolidated;
                record.Active = updaterecord.Active;
                record.Sessionnote = updaterecord.Sessionnote;

                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/api/session/{id}", async (Db db, string id) =>
            {
                var record = await db.Sessions
                                    .Where(a => a.Id == id && a.Deleted == 0)
                                    .FirstOrDefaultAsync();
                if (record is null)
                {
                    return Results.NotFound();
                }
                record.Deleted = 1;
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            /* ------------------------------------------------- */


        }
    }
}