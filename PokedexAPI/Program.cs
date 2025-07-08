using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://olli-pokedexwebapp.vercel.app", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

 
var connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DefaultConnection") ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PokedexContext>(options => options.UseNpgsql(connectionString));



//Use in memory database for testing
//builder.Services.AddDbContext<PokedexContext>(options => options.UseInMemoryDatabase("PokedexDB"));

builder.Services.AddScoped<PokemonBaseService>();
builder.Services.AddScoped<PokemonVariantService>();
builder.Services.AddScoped<PokemonTypeService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PokedexContext>();
    db.Database.Migrate(); // Apply migrations to db, useful for when render db expires and new one is set up
}



// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
