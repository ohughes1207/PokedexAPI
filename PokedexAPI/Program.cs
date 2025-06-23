using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Frontend dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container.

//builder.Services.AddDbContext<PokedexContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Use in memory database for testing
builder.Services.AddDbContext<PokedexContext>(options => options.UseInMemoryDatabase("PokedexDB"));

builder.Services.AddScoped<PokemonBaseService>();
builder.Services.AddScoped<PokemonVariantService>();
builder.Services.AddScoped<PokemonTypeService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
