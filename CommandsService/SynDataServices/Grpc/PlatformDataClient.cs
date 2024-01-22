using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SynDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public IEnumerable<Platform>? ReturnAllPlatform()
        {
            Console.WriteLine($"--> Calling gRPC Service {_configuration["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]!);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try
            {
                var rely = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(rely.Platform);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not call gRPC server {ex.Message}");
                return null;
            }
        }
    }
}