using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class PositionSetupController
    {
        public static void MapEndpoints_PositionSetups(this WebApplication app)
        {
            app.MapGet("/api/position_setups", async (Db db) => await db.Position_setups
                                                                .Where(a => a.Deleted == 0 && a.Active == 1)
                                                                .ToListAsync());                    
        }
    }
}