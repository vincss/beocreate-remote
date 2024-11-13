using BeocreateRemote.Core;
using Microsoft.AspNetCore.Mvc;

namespace BeocreateRemote.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeocreateController(ILogger<BeocreateController> logger, IRemoteController remoteController) : ControllerBase
    {

        [HttpGet]
        public int Get([FromQuery] string? action)
        {
            try
            {
                if (action == "mute")
                {
                    var mute = !remoteController.Mute;
                    remoteController.Mute = mute;
                    logger.LogInformation($"Get action:{action}");
                    return mute ? 1 : 0;
                }
                var volume = remoteController.Volume;
                int? newVolume = action == null ? null : remoteController.Volume += action == "plus" ? 1 : -1;
                logger.LogInformation($"Get action:{action} volume:{volume} newVolume:{newVolume}");
                if (newVolume != null)
                {
                    remoteController.Volume = newVolume.Value;
                }

                return newVolume ?? volume;
            }
            catch (Exception e)
            {
                logger.LogError($"ERROR Get failed to act on volume: {e.Message}");
            }
            return 0;
        }
    }

}
