using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using  GeneralStore.Repositories;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class AccountController
    {
        public static void MapAccountsEndpoints(this WebApplication app)
        {
            app.MapGet("/api/accounts", async (Db db) => await db.Accounts
                                                                .Where(a => a.Deleted == 0)
                                                                .ToListAsync());

            app.MapGet("/api/account/{id}", async (Db db, int id) => await db.Accounts
                                                                            .Where(a => a.Id == id && a.Deleted == 0)
                                                                            .FirstOrDefaultAsync());    

            app.MapPost("/api/account", async (Db db, Account record) =>
            {
                await db.Accounts.AddAsync(record);
                await db.SaveChangesAsync();
                return Results.Created($"/record/{record.Id}", record);
            });
            app.MapPut("/api/account/{id}", async (Db db, Account updaterecord, int id) =>
            {
                var account = await db.Accounts
                                            .Where(a => a.Id == id && a.Deleted == 0)
                                            .FirstOrDefaultAsync();
                if (account is null) return Results.NotFound();

                //account.Modification = "xx-xx-xxxx";
                
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
                //db.Items.Remove(item);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}