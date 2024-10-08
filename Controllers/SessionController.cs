using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class SessionController
    {
        public static void MapEndpoints_Sessions(this WebApplication app)
        {            
            app.MapGet("/api/sessions", async (GeneralStore.Interfaces.ISession repo) => await repo.GetAllAsync());
            app.MapGet("/api/session/{id}", async (GeneralStore.Interfaces.ISession repo, int id) => await repo.GetAsync(id));
            app.MapGet("/api/session/last", async (GeneralStore.Interfaces.ISession repo) => await repo.GetLastAsync());

            app.MapPost("/api/session/createSessionIfNotExists/{id}", async (Interfaces.ISession repo, int id) =>
            {
                var newItem = await repo.CreateAsync(id);
                return Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/session/{id}", async (GeneralStore.Interfaces.ISession repo, Session updaterecord, int id) =>
            {
                var updatedItem = await repo.UpdateAsync(updaterecord, id);
                if (updatedItem is null) return Results.NotFound();
                return Results.NoContent();
            });

            app.MapDelete("/api/session/{id}", async (GeneralStore.Interfaces.ISession repo, int id) =>
            {
                var item = await repo.DeleteAsync(id);
                if (item is null) return Results.NotFound();
                return Results.Ok();
            });
            
        }
    }
}