using BeocreateRemote.Core;
using Microsoft.AspNetCore.Mvc;

namespace BeocreateRemote.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeocreateController : ControllerBase
    {
        private readonly ILogger<BeocreateController> _logger;
        private readonly IRemoteController remoteController;

        public BeocreateController(ILogger<BeocreateController> logger, IRemoteController remoteController)
        {
            _logger = logger;
            this.remoteController = remoteController;
        }


        [HttpGet]
        public int Get([FromQuery] string? action)
        {
            var volume = remoteController.Volume;
            int? newVolume = action == null ? null : remoteController.Volume += action == "plus" ? 1 : -1;
            _logger.LogInformation($"GetVolume action:{action} volume:{volume} newVolume:{newVolume}");
            if (newVolume != null) 
            {
               remoteController.Volume = newVolume.Value;
            }

            return newVolume != null ? newVolume.Value : volume ;
        }
    }
}
