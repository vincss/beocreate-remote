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
            try
            {
                if (action == "mute")
                {
                    remoteController.ToggleMute();
                    _logger.LogInformation($"Get action:{action}");
                    return 0;
                }
                var volume = remoteController.Volume;
                int? newVolume = action == null ? null : remoteController.Volume += action == "plus" ? 1 : -1;
                _logger.LogInformation($"Get action:{action} volume:{volume} newVolume:{newVolume}");
                if (newVolume != null)
                {
                    remoteController.Volume = newVolume.Value;
                }

                return newVolume != null ? newVolume.Value : volume;
            }
            catch (System.Exception e)
            {
                _logger.LogError($"ERROR Get failed to act on volume: {e.Message}");
            }
            return 0;
        }
    }
}
