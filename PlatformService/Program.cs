using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Repositories;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Access the Configuration object
var configuration = builder.Configuration;
Console.WriteLine($"--> Command server endpoint: {configuration["CommandsService"]}");

// Add services to the container.
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();
// Add auto maper service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
    options.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapGrpcService<GrpcPlatformService>();
app.MapGet("/Protos/platforms.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("/Protos/platforms.proto"));
});

// Add data for testing
PreparationDb.PreparationPopulation(app, app.Environment.IsProduction());

app.Run();
