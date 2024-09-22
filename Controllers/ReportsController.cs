using GeneralStore.Models;
using GeneralStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace GeneralStore.Controllers
{
    public static class ReportsController
    {
        public static void MapEndpoints_Reports(this WebApplication app)
        {

            app.MapGet("/api/reports/getTest", (IDataTable repo) => repo.GetTest()); 
            app.MapGet("/api/reports/getPositions", async (IDataTable repo) => await repo.GetPositionsAsync());

            //app.MapGet("/api/reports/getPositionsSearch", async (IDataTable repo) => await repo.GetPositionsSearchAsync("str"));
            //app.MapGet("/api/reports/getPositionsSearch", async (IDataTable repo) => await repo.GetPositionsSearchAsync(searchOptions));
            //app.MapPost("/api/reports/getPositionsSearch", async (IDataTable repo, string? searchOptions) =>
            app.MapPost("/api/reports/getPositionsSearch", async (IDataTable repo, SearchOptions? searchOptions) =>
            {
                var response = await repo.GetPositionsSearchAsync(searchOptions);
                return response;
            });

        }
    }
}