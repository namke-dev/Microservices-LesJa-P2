using AutoMapper;
using CommandsService.Repositories;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandsRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine("--> Hit GetAllPlatforms from Commands Service");
            var platfromsItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platfromsItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            //access url: http://localhost:5266/api/c/platforms to test endpoint
            Console.WriteLine("--> Inbound POST # Commands Service");
            return Ok("Inbound test PlatformsController of CommandsService");
        }
    }
}