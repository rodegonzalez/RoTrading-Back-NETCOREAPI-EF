using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class BrokerController
    {
        public static void MapEndpoints_Brokers(this WebApplication app)
        {
            app.MapGet("/api/brokers", async (Db db) => await db.Brokers
                                                                .Where(a => a.Deleted == 0)
                                                                .Take(100)
                                                                .ToListAsync());            

            app.MapGet("/api/broker/{id}", async (Db db, int id) => await db.Brokers
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                            .FirstOrDefaultAsync());    

            app.MapPost("/api/broker", async (Db db, Broker record) =>
            {
                record.Modification = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                await db.Brokers.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/broker/{id}", async (Db db, Broker updaterecord, int id) =>
            {
                var record = await db.Brokers
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
            app.MapDelete("/api/broker/{id}", async (Db db, int id) =>
            {
                var record = await db.Brokers
                                    .Where(a => a.Id == id && a.Deleted == 0)
                                    .FirstOrDefaultAsync();
                if (record is null)
                {
                    return Results.NotFound();
                }
                record.Deleted = 1;
                record.Modification = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                record.Name = $"{record.Name}_[{record.Id}_{record.Modification}]_Deleted";                
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}