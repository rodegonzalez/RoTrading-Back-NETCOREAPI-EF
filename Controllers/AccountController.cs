using GeneralStore.Models;
using GeneralStore.Interfaces;
using GeneralStore.Services;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Controllers
{
    public static class AccountController
    {
        public static void MapEndpoints_Accounts(this WebApplication app)
        {

            /* ---------------------------------- */
            /*
            app.MapGet("/api/accounts", async (IAccount repo) => await repo.GetAllAsync());

            app.MapGet("/api/account/{id}", async (IAccount repo, int id) => await repo.GetItemByIdAsync(id));

            app.MapPost("/api/account", async (AccountService service, Account record) =>
            {
                var newItem = await service.CreateItemAsync(record);
                return (newItem is null) ? Results.NotFound() : Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/account/{id}", async (AccountService service, Account updaterecord, int id) =>
            {
                var updatedItem = await service.UpdateItemAsync(updaterecord, id);
                return (updatedItem is null)  ? Results.NotFound() : Results.NoContent();
            });

            app.MapDelete("/api/account/{id}", async (AccountService service, int id) =>
            {
                var item = await service.DeleteItemAsync(id);
                return (item is null) ? Results.NotFound() : Results.Ok();
            });
            */

            /* ---------------------------------- */
            /* ---------------------------------- */

            
            app.MapGet("/api/accounts", async (Db db) => await db.Accounts
                                                                .Where(a => a.Deleted == 0)
                                                                .ToListAsync());

            app.MapGet("/api/account/{id}", async (Db db, int id) => await db.Accounts
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                          //.FirstOrDefaultAsync());    
                                                                          .Take(1)
                                                                          .ToListAsync());

            app.MapPost("/api/account", async (Db db, Account record) =>
            {
                record.Creation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Modification = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                record.Active = 1;
                record.Deleted = 0;
                await db.Accounts.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/account/{id}", async (Db db, Account updaterecord, int id) =>
            {
                var record = await db.Accounts
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (record is null) return Results.NotFound();

                record.Name = updaterecord.Name;
                record.Description = updaterecord.Description;
                record.Status = updaterecord.Status;
                record.Active = updaterecord.Active;
                record.Note = updaterecord.Note;
                record.Modification = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                record.Amount_initial = updaterecord.Amount_initial;
                record.Amount_current = updaterecord.Amount_current;
                record.Divisaid = updaterecord.Divisaid;
                record.Acctype = updaterecord.Acctype;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/account/{id}", async (Db db, int id) =>
            {
                var record = await db.Accounts
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
            

            /* ---------------------------------- */



        }
    }
}