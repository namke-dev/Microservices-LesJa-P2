using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandsRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommandsForPlatform(int platformId)
        {
            Console.WriteLine("--> Hit GetAllCommandsForPlatform from Commands Service");
            if (!_repository.IsPlatfromExist(platformId))
            {
                return NotFound();
            }
            var commandItems = _repository.GetAllCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));

        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine("--> Hit GetCommandForPlatform from Commands Service");
            if (!_repository.IsPlatfromExist(platformId))
            {
                return NotFound();
            }

            var commandItem = _repository.GetCommand(platformId, commandId);

            if (commandItem is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine("--> Hit CreateCommandForPlatform from Commands Service");
            if (!_repository.IsPlatfromExist(platformId))
            {
                return NotFound();
            }

            var commandItem = _mapper.Map<Command>(commandCreateDto);

            _repository.CreateCommand(platformId, commandItem);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandItem);
            return CreatedAtRoute(
                nameof(GetCommandForPlatform),
                new { platformId = platformId, commandId = commandReadDto.Id },
                commandReadDto);

        }
    }
}