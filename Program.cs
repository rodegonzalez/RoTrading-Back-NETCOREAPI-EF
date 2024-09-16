using GeneralStore.Controllers;
using GeneralStore.Extensions;
using GeneralStore.Models;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Db
    var connectionString = builder.Configuration.GetConnectionString("sqlitedb") ?? "Data Source=../rotrading.db";
    builder.Services.AddSqlite<Db>(connectionString);    
    //services.AddDbContext<Db>(options => options.UseSqlite("Data Source=../rotrading.db"));

    // Call ConfigureServices method from ServiceExtensions.cs
    builder.Services.ConfigureServices();

    var app = builder.Build();

    // dev environment
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
    }

    app.UseCors("AllowSpecificOrigin_localhost"); // CORS policy to use
    app.MapAllEndpoints(); // API Endpoints

    // Url and port configuration
    var kestrelConfig = builder.Configuration.GetSection("Kestrel:Endpoints:Http:Url").Value;
    if (!string.IsNullOrEmpty(kestrelConfig))
    {
        app.Urls.Add(kestrelConfig);
    }
    
    app.Run();

}
catch(Exception ex)
{
    // Log the exception
    Console.WriteLine($"Error: {ex.Message}");
    throw;
}
