using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;

namespace GeneralStore.Controllers
{
    public static class TickeraccountController
    {
        public static void MapEndpoints_Tickeraccounts(this WebApplication app)
        {
            app.MapGet("/api/tickeraccounts", async (Db db) => await db.Tickeraccounts.ToListAsync());                                                                

            app.MapGet("/api/tickeraccount/{tickerid}/{accountid}", async (Db db, int tickerid, int accountid) => await db.Tickeraccounts
                                                                            .Where(a => a.Tickerid == tickerid && a.Accountid == accountid)
                                                                          //.FirstOrDefaultAsync());    
                                                                          .Take(1)
                                                                          .ToListAsync());
        }
    }
}