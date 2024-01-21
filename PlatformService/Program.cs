using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Repositories;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

// Add auto maper service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Access the Configuration object
var configuration = builder.Configuration;
Console.WriteLine($"--> Command server endpoint: {configuration["CommandsService"]}");

// Add Db Context service
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMem"));

}
else if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("PlatformsConn"))
    );
}

var app = builder.Build();


if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Add data for testing
PreparationDb.PreparationPopulation(app, app.Environment.IsProduction());

app.Run();
