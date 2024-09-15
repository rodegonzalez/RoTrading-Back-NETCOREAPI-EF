using GeneralStore.Models;
using GeneralStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Controllers
{
    public static class AccountController
    {
        public static void MapEndpoints_Accounts(this WebApplication app)
        {
            
            app.MapGet("/api/accounts", async (IAccount repo) => await repo.GetAllAsync());

            app.MapGet("/api/account/{id}", async (IAccount repo, int id) => await repo.GetAsync(id));

            app.MapPost("/api/account", async (IAccount repo, Account record) =>
            {
                var newItem = await repo.CreateAsync(record);
                return (newItem is null) ? Results.NotFound() : Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/account/{id}", async (IAccount repo, Account updaterecord, int id) =>
            {
                var updatedItem = await repo.UpdateAsync(updaterecord, id);
                return (updatedItem is null)  ? Results.NotFound() : Results.NoContent();
            });

            app.MapDelete("/api/account/{id}", async (IAccount repo, int id) =>
            {
                var item = await repo.DeleteAsync(id);
                return (item is null) ? Results.NotFound() : Results.Ok();
            });            

        }
    }
}