using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class PositionPatternController
    {
        public static void MapEndpoints_PositionPatterns(this WebApplication app)
        {
            // Paterns ---------------------------------------------------------------------------------------------------------
            // Common patterns: Giro, Cont, Fac 
            app.MapGet("/api/position_patterns", async (Db db) => await db.Position_patterns
                                                                .Where(a => a.Deleted == 0 && a.Active == 1)
                                                                .ToListAsync());

            // High Patterns ----------------------------------------------------------------------------------------------------
            // User custom patterns
            app.MapGet("/api/position_highpatterns", async (IPositionHighPattern repo) => await repo.GetAllAsync());


            app.MapGet("/api/position_highpattern/{id}", async (IPositionHighPattern repo, int id) => await repo.GetAsync(id));

            app.MapPost("/api/position_highpattern", async (IPositionHighPattern repo, Position_highpattern record) =>
            {
                var newItem = await repo.CreateAsync(record);
                return (newItem is null) ? Results.NotFound() : Results.Created($"/record/{newItem.Id}", newItem);
            });

            app.MapPut("/api/position_highpattern/{id}", async (IPositionHighPattern repo, Position_highpattern updaterecord, int id) =>
            {
                var updatedItem = await repo.UpdateAsync(updaterecord, id);
                return (updatedItem is null) ? Results.NotFound() : Results.NoContent();
            });

            app.MapDelete("/api/position_highpattern/{id}", async (IPositionHighPattern repo, int id) =>
            {
                var item = await repo.DeleteAsync(id);
                return (item is null) ? Results.NotFound() : Results.Ok();
            });



        }

    }


}