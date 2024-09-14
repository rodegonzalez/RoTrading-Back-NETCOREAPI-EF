using BK_NetAPI_SQLite.Interfaces;
using BK_NetAPI_SQLite.Repositories;
using BK_NetAPI_SQLite.Services;
using GeneralStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
namespace BK_NetAPI_SQLite.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Agrega aquí todas las configuraciones de servicios

            // Db
            //var connectionString = builder.Configuration.GetConnectionString("rotrading") ?? "Data Source=../rotrading.db";
            //builder.Services.AddSqlite<Db>(connectionString);
            services.AddDbContext<Db>(options => options.UseSqlite("Data Source=../rotrading.db"));

            // Repositories
            services.AddScoped<IPosition, PositionRepository>();
            services.AddScoped<PositionService>();

            services.AddScoped<IPositionsSession, PositionsSessionRepository>();
            services.AddScoped<PositionsSessionService>();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin_localhost", // Policy name
                    builder =>
                    {
                        builder.SetIsOriginAllowed(origin => {
                            var uri = new Uri(origin);
                            return uri.Host.StartsWith("192.168.0.") || uri.Host == "localhost";
                        })
                               .AllowAnyMethod() // All methods (GET, POST, etc.)
                               .AllowAnyHeader(); // All headers
                    });
            });

        }
    }
}
