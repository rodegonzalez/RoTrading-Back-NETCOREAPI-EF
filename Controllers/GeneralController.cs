using GeneralStore.Controllers;

namespace GeneralStore.Controllers
{
    public static class GeneralController
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
           app.MapItemsEndpoints();
        }
    }
}