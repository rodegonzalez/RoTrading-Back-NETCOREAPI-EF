using GeneralStore.Models;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class TppController
    {
        public static void MapEndpoints_Tpps(this WebApplication app)
        {
            app.MapGet("/api/tpps", async (ITpp repo) => await repo.GetAllAsync());

            app.MapGet("/api/tpp/{id}", async (ITpp repo, int id) => await repo.GetAsync(id));

            app.MapGet("/api/tpp/getsecuence/{id}", async (ITpp repo, int id) => repo.GetSecuenceAsync(id));

            app.MapPost("/api/tpp", async (ITpp repo, Tpp record) =>
            {
                var newItem = await repo.CreateAsync(record);
                return (newItem is null) ? Results.NotFound() : Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/tpp/{id}", async (ITpp repo, Tpp updaterecord, int id) =>
            {
                var updatedItem = await repo.UpdateAsync(updaterecord, id);
                return (updatedItem is null) ? Results.NotFound() : Results.NoContent();
            });

            app.MapDelete("/api/tpp/{id}", async (ITpp repo, int id) =>
            {
                var item = await repo.DeleteAsync(id);
                return (item is null) ? Results.NotFound() : Results.Ok();
            });
        }
    }
}