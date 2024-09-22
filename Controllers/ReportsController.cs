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

            app.MapPost("/api/reports/getPositionsSearch", async (IDataTable repo, [FromQuery] string options) => await repo.GetPositionsSearchAsync(options));
        }
    }
}