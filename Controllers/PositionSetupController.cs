using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class PositionSetupController
    {
        public static void MapEndpoints_PositionSetups(this WebApplication app)
        {
            app.MapGet("/api/position_setups", async (IPositionSetup repo) => await repo.GetAllAsync());

            app.MapGet("/api/position_setup/{id}", async (IPositionSetup repo, int id) => await repo.GetAsync(id));

            app.MapPost("/api/position_setup", async (IPositionSetup repo, Position_setup record) =>
            {
                var newItem = await repo.CreateAsync(record);
                return (newItem is null) ? Results.NotFound() : Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/position_setup/{id}", async (IPositionSetup repo, Position_setup updaterecord, int id) =>
            {
                var updatedItem = await repo.UpdateAsync(updaterecord, id);
                return (updatedItem is null) ? Results.NotFound() : Results.NoContent();
            });

            app.MapDelete("/api/position_setup/{id}", async (IPositionSetup repo, int id) =>
            {
                var item = await repo.DeleteAsync(id);
                return (item is null) ? Results.NotFound() : Results.Ok();
            });

        }
    }
}