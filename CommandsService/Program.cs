using CommandsService.AsyncDataServices;
using CommandsService.Data;
using CommandsService.EventProcessing;
using CommandsService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICommandsRepo, CommandsRepo>();

// Add Event Processor service
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

// Add auto maper service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Db Context service
Console.WriteLine("--> Using InMem Db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMem"));


builder.Services.AddControllers();
// Add MessageBusSubscriber Service
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();

app.Run();
