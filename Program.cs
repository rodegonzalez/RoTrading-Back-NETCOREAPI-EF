using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ItemsStore.Models;
using ItemsStore.Controllers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Items") ?? "Data Source=Items.db";
builder.Services.AddSqlite<Db>(connectionString);


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });

var app = builder.Build();

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
	} 


app.MapItemsEndpoints(); // Registra los endpoints de ItemsController
app.Urls.Add("http://localhost:5100"); // Inicia en puerto 5000
app.Run();
