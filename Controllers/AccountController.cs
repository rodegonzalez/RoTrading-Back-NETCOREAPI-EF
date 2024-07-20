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
                                                                .Where(a => a.deleted == 0 && a.active == 1)
                                                                .ToListAsync());

            app.MapGet("/api/account/{id}", async (Db db, int id) => await db.Accounts
                                                                            .Where(a => a.Idaccount == id && a.deleted == 0 && a.active == 1)
                                                                            .FirstOrDefaultAsync());    

            app.MapPost("/api/account", async (Db db, Account account) =>
            {
                await db.Accounts.AddAsync(account);
                await db.SaveChangesAsync();
                return Results.Created($"/account/{account.Idaccount}", account);
            });
            app.MapPut("/api/account/{id}", async (Db db, Account updateaccount, int id) =>
            {
                var account = await db.Accounts
                                            .Where(a => a.Idaccount == id && a.deleted == 0 && a.active == 1)
                                            .FirstOrDefaultAsync();
                if (account is null) return Results.NotFound();

                //account.Modification = "xx-xx-xxxx";
                
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/api/account/{id}", async (Db db, int id) =>
            {
                var item = await db.Accounts
                                    .Where(a => a.Idaccount == id && a.deleted == 0 && a.active == 1)
                                    .FirstOrDefaultAsync();
                if (item is null)
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