using GeneralStore.Controllers;
using GeneralStore.Interfaces;
using GeneralStore.Repositories;

namespace GeneralStore.Controllers
{
    public static class MainController
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
           app.MapItemsEndpoints();
           app.MapAccountsEndpoints();
        }
    }
}