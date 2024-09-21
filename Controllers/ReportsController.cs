using GeneralStore.Models;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class ReportsController
    {
        public static void MapEndpoints_Reports(this WebApplication app)
        {

            app.MapGet("/api/reports/getTest", (IDataTable repo) => repo.GetTest()); 
            app.MapGet("/api/reports/getPositions", async (IDataTable repo) => await repo.GetPositionsAsync());       

        }
    }
}