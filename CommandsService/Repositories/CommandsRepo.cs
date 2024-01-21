using CommandsService.Data;
using CommandsService.Models;

namespace CommandsService.Repositories
{
    public class CommandsRepo : ICommandsRepo
    {
        private readonly AppDbContext _context;

        public CommandsRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            else
            {
                _context.Platforms.Add(platform);
            }
        }

        public bool IsPlatfromExist(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform!.Name)
                .ToList();
        }

        public Command? GetCommand(int platformId, int commandId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId && c.Id == commandId)
                .FirstOrDefault();
        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            else if (IsPlatfromExist(platformId))
            {
                command.PlatformId = platformId;
                _context.Commands.Add(command);
            }
            else
            {
                throw new ArgumentException("The Platform does not exist");
            }
        }
    }
}