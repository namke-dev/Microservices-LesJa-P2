using PlatformService.Models;

namespace PlatformService.Repositories
{
    public interface IPlatformRepo
    {
        bool SaveChange();

        IEnumerable<Platform> GetAllPlatforms();
        Platform? GetPlatformById(int id);
        void CeatePlatform(Platform platform);

    }
}