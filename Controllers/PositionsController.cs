using GeneralStore.Interfaces;
using GeneralStore.Services;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class PositionController
    {
        public static void MapEndpoints_Positions(this WebApplication app)
        {
            app.MapGet("/api/positions", async (IPosition repo) => await repo.GetAllPositionsAsync());
            app.MapGet("/api/positions/opened", async (IPosition repo) => await repo.GetOpenedPositionsAsync());
            app.MapGet("/api/positions/notopened", async (IPosition repo) => await repo.GetNotOpenedPositionsAsync());
            app.MapGet("/api/position/{id}", async (IPosition repo, int id) => await repo.GetPositionByIdAsync(id));

            app.MapPost("/api/position", async (PositionService service, Position record) =>
            {
                var newPosition = await service.CreatePositionAsync(record);
                return Results.Created($"/record/{newPosition.Id}", newPosition);
            });

            app.MapPut("/api/position/{id}", async (PositionService service, Position updaterecord, int id) =>
            {
                var updatedPosition = await service.UpdatePositionAsync(updaterecord, id);
                if (updatedPosition is null) return Results.NotFound();
                return Results.NoContent();
            });

            app.MapDelete("/api/position/{id}", async (PositionService service, int id) =>
            {
                var position = await service.DeletePositionAsync(id);
                if (position is null) return Results.NotFound();
                return Results.Ok();
            });
        }

    } // end class
}

