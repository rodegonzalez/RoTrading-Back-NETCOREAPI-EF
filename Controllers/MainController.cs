using GeneralStore.Controllers;
using GeneralStore.Interfaces;

namespace GeneralStore.Controllers
{
    public static class MainController
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
           app.MapItemsEndpoints();
           app.MapEndpoints_Accounts();
           app.MapEndpoints_Positions();
           app.MapEndpoints_Brokers();
           app.MapEndpoints_Tpps();
           app.MapEndpoints_PositionSetups();
           app.MapEndpoints_PositionPatterns();
           app.MapEndpoints_Tickers();            
        }
    }
}