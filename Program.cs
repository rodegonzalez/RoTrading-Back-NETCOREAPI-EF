using GeneralStore.Controllers;
using GeneralStore.Extensions;
using GeneralStore.Repositories;
using GeneralStore.Interfaces;
using GeneralStore.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

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
    app.Urls.Add("http://localhost:5100"); // Start on port 5000
    app.Run();

}
catch(Exception ex)
{
    // Log the exception
    Console.WriteLine($"Error: {ex.Message}");
    throw;
}
