using CommandsService.Models;
using CommandsService.Repositories;
using CommandsService.SynDataServices.Grpc;

namespace CommandsService.Data
{
    public class PreparationDb
    {
        public static void PreparePopulation(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

            var platforms = grpcClient!.ReturnAllPlatforms();
            SeedData(serviceScope.ServiceProvider.GetService<ICommandsRepo>()!, platforms!);

        }

        private static void SeedData(ICommandsRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("--> Seeding new platform . . .");

            foreach (var platform in platforms)
            {
                if (!repo.IsExternalPlatformExist(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                }
            }
            repo.SaveChanges();
        }
    }
}