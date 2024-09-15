using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using  GeneralStore.Repositories;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class TickerController
    {
        public static void MapEndpoints_Tickers(this WebApplication app)
        {
            app.MapGet("/api/tickers", async (ITicker repo) => await repo.GetAllAsync());

            app.MapGet("/api/ticker/{id}", async (ITicker repo, int id) => await repo.GetAsync(id));

            app.MapPost("/api/ticker", async (ITicker repo, Ticker record) =>
            {
                var newItem = await repo.CreateAsync(record);
                return (newItem is null) ? Results.NotFound() : Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/ticker/{id}", async (ITicker repo, Ticker updaterecord, int id) =>
            {
                var updatedItem = await repo.UpdateAsync(updaterecord, id);
                return (updatedItem is null) ? Results.NotFound() : Results.NoContent();
            });

            app.MapDelete("/api/ticker/{id}", async (ITicker repo, int id) =>
            {
                var item = await repo.DeleteAsync(id);
                return (item is null) ? Results.NotFound() : Results.Ok();
            });

        }
    }
}