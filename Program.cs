using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ItemsStore.Models;

// DB 

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Items") ?? "Data Source=Items.db";
builder.Services.AddSqlite<Db>(connectionString);


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });

    // Add API tests
    builder.Services.AddHttpClient();
    builder.Services.AddMvc().AddApplicationPart(typeof(ApiTests).Assembly);

var app = builder.Build();

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
	} 

app.MapGet("/api/items", async (Db db) => await db.Items.ToListAsync());
app.MapGet("/api/item/{id}", async (Db db, int id) => await db.Items.FindAsync(id));    

app.MapPost("/api/item", async (Db db, Item item) =>
{
    await db.Items.AddAsync(item);
    await db.SaveChangesAsync();
    return Results.Created($"/item/{item.Id}", item);
});

app.MapPut("/api/item/{id}", async (Db db, Item updateitem, int id) =>
{
      var item = await db.Items.FindAsync(id);
      if (item is null) return Results.NotFound();
      item.ItemName = updateitem.ItemName;
      item.ItemValue = updateitem.ItemValue;
      await db.SaveChangesAsync();
      return Results.NoContent();
});

app.MapDelete("/api/item/{id}", async (Db db, int id) =>
{
   var item = await db.Items.FindAsync(id);
   if (item is null)
   {
      return Results.NotFound();
   }
   db.Items.Remove(item);
   await db.SaveChangesAsync();
   return Results.Ok();
});

app.Urls.Add("http://localhost:5100");
app.Run();
