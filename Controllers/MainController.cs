using GeneralStore.Controllers;

namespace GeneralStore.Controllers
{
    public static class MainController
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
           app.MapItemsEndpoints();
        }
    }
}