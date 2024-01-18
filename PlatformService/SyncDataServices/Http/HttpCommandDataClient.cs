using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        public HttpCommandDataClient(HttpClient httpClient)
        {

        }
        public Task SendPlatformToCommand(PlatformReadDto platform)
        {
            throw new NotImplementedException();
        }
    }
}