using System.Text.Json;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Repositories;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory,
            IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DeterminEvent(message);
            switch (eventType)
            {
                case EventType.Platform_Published:
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }

        private static EventType DeterminEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType?.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("--> Platform published event detected");
                    return EventType.Platform_Published;
                default:
                    Console.WriteLine("--> Could not determin the event type");
                    return EventType.undetermined;
            }
        }

        private void addPlatform(string platformPublishedMessage)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ICommandsRepo>();

            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

            try
            {
                var platform = _mapper.Map<Platform>(platformPublishedDto);
                if (!repo.IsExternalPlatformExist(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                    repo.SaveChanges();
                    Console.WriteLine("--> Platform Added to DB . . .");
                }
                else
                {
                    Console.WriteLine("--> Platform already exist . . .");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not add platform to DB {e.Message}");
            }
        }
    }

    enum EventType
    {
        Platform_Published,
        undetermined
    }
}