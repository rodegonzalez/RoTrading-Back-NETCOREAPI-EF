using GeneralStore.Interfaces;
using GeneralStore.Repositories;
using Microsoft.OpenApi.Models;

namespace GeneralStore.Extensions
{
    public static class _ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {           
            // Repositories
            services.AddScoped<IPosition, PositionRepository>();
            services.AddScoped<Interfaces.ISession, SessionRepository>();
            services.AddScoped<IAccount,AccountRepository>();
            services.AddScoped<ITicker, TickerRepository>();
            services.AddScoped<ITpp, TppRepository>();
            services.AddScoped<IDivisa, DivisaRepository>();
            services.AddScoped<IPositionSetup, PositionSetupRepository>();
            services.AddScoped<IPositionHighPattern, PositionHighPatternRepository>();

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
