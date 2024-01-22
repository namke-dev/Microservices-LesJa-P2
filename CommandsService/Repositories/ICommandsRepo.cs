using CommandsService.Models;

namespace CommandsService.Repositories
{
    public interface ICommandsRepo
    {
        bool SaveChanges();

        // Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool IsPlatfromExist(int platformId);
        bool IsExternalPlatformExist(int externalPlatformId);

        // Commands
        IEnumerable<Command> GetAllCommandsForPlatform(int platformId);
        Command? GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);


    }
}