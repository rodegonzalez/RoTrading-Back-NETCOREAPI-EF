using GeneralStore.Interfaces;
using GeneralStore.Repositories;
using GeneralStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeneralStore.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {           
            // Repositories
            services.AddScoped<IPosition, PositionRepository>();
            services.AddScoped<Interfaces.ISession, SessionRepository>();
            services.AddScoped<IAccount,AccountRepository>();

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
