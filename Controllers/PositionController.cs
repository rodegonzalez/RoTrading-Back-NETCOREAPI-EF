using GeneralStore.Interfaces;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class PositionController
    {
        public static void MapEndpoints_Positions(this WebApplication app)
        {
            app.MapGet("/api/positions", async (IPosition repo) => await repo.GetAllAsync());
            app.MapGet("/api/positions/opened", async (IPosition repo) => await repo.GetOpenedPositionsAsync());
            app.MapGet("/api/positions/notopened", async (IPosition repo) => await repo.GetNotOpenedPositionsAsync());
            app.MapGet("/api/position/{id}", async (IPosition repo, int id) => await repo.GetAsync(id));

            app.MapPost("/api/position", async (IPosition repo, Position record) =>
            {
                var newPosition = await repo.CreateAsync(record);
                return Results.Created($"/record/{newPosition.Id}", newPosition);
            });

            app.MapPut("/api/position/{id}", async (IPosition repo, Position updaterecord, int id) =>
            {
                updaterecord.Status = "opened";
                var updatedPosition = await repo.UpdateAsync(updaterecord, id);
                if (updatedPosition is null) return Results.NotFound();
                return Results.NoContent();
            });

            app.MapPut("/api/position-close/{id}", async (IPosition repo, Position updaterecord, int id) =>
            {
                updaterecord.Status = "closed";
                var updatedPosition = await repo.UpdateAsync(updaterecord, id);
                if (updatedPosition is null) return Results.NotFound();
                return Results.NoContent();
            });

            app.MapDelete("/api/position/{id}", async (IPosition repo, int id) =>
            {
                var position = await repo.DeleteAsync(id);
                if (position is null) return Results.NotFound();
                return Results.Ok();
            });
        }

    } // end class
}

