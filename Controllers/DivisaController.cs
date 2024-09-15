using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using System.Diagnostics.Eventing.Reader;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class DivisaController
    {
        public static void MapEndpoints_Divisas(this WebApplication app)
        {
            app.MapGet("/api/divisas", async (IDivisa repo) => await repo.GetAllAsync());

            app.MapGet("/api/divisa/{id}", async (IDivisa repo, int id) => await repo.GetAsync(id));

            app.MapPost("/api/divisa", async (IDivisa repo, Divisa record) =>
            {
                var newItem = await repo.CreateAsync(record);
                return (newItem is null) ? Results.NotFound() : Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/divisa/{id}", async (IDivisa repo, Divisa updaterecord, int id) =>
            {
                var updatedItem = await repo.UpdateAsync(updaterecord, id);
                return (updatedItem is null) ? Results.NotFound() : Results.NoContent();
            });

            app.MapDelete("/api/divisa/{id}", async (IDivisa repo, int id) =>
            {
                var item = await repo.DeleteAsync(id);
                return (item is null) ? Results.NotFound() : Results.Ok();
            });

        }
    }
}