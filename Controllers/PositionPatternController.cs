using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class PositionPatternController
    {
        public static void MapEndpoints_PositionPatterns(this WebApplication app)
        {
            app.MapGet("/api/position_patterns", async (Db db) => await db.Position_patterns
                                                                .Where(a => a.Deleted == 0 && a.Active == 1)
                                                                .ToListAsync());                    
        }
    }
}