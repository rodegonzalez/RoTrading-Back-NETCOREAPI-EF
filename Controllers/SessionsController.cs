using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using System.Linq;

namespace GeneralStore.Controllers
{
    public static class SessionController
    {
        public static void MapEndpoints_Sessions(this WebApplication app)
        {
            app.MapGet("/api/sessions", async (Db db) => await db.Sessions.ToListAsync());                                                                

            app.MapGet("/api/session/{id}", async (Db db, int id) => await db.Sessions
                                                                           .Where(a => a.Id == id)
                                                                          .FirstOrDefaultAsync());    
                                                                         
            app.MapGet("/api/session/last", async (Db db) => await db.Sessions
                                                             .OrderByDescending(a => a.Id)
                                                             .FirstOrDefaultAsync());                                                                              
        }
    }
}