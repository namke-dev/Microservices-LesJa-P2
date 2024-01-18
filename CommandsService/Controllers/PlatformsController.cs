using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }
        [HttpGet]
        public ActionResult TestInboundConnection()
        {
            //access url: http://localhost:5266/api/c/platforms to test endpoint
            Console.WriteLine("--> Inbound POST # Commands Service");
            return Ok("Inbound test PlatformsController of CommandsService");
        }
    }
}