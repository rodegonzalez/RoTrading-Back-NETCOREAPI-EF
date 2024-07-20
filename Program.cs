using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Controllers;
using GeneralStore.Interfaces;
using GeneralStore.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("rotrading") ?? "Data Source=rotrading.db";
builder.Services.AddSqlite<Db>(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", // Nombre de la política
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Especifica los orígenes permitidos
                   .AllowAnyMethod() // Permite cualquier método (GET, POST, etc.)
                   .AllowAnyHeader(); // Permite cualquier encabezado
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
} 

app.UseCors("AllowSpecificOrigin"); // Usa la política de CORS definida anteriormente

//builder.Services.AddScoped<IItemRepository, ItemRepository>(); // Registra el servicio de ItemsRepository
app.MapAllEndpoints(); // Registra los endpoints de ItemsController
app.Urls.Add("http://localhost:5100"); // Inicia en puerto 5000
app.Run();
