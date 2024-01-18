using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5266/api/c/platforms", httpContent);
            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("--> Sync POST to CommandsService succeeded");
            }
            else
            {
                System.Console.WriteLine("--> Sync POST to CommandsService failded");
            }
        }
    }
}