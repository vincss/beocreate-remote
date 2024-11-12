using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Device.Pwm.Drivers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeocreateRemote.Core
{
    public class FanController : IDisposable
    {
        const int Pin = 12;
        private readonly ILogger<FanController> _logger;
        private readonly SoftwarePwmChannel _pwm;

        public FanController(ILogger<FanController> logger)
        {
            _logger = logger;
            _pwm = new SoftwarePwmChannel(Pin, 50, 1);
            _pwm.Start();
        }

        public void Set(double speed)
        {
            _pwm.DutyCycle = speed;
        }

        public void Dispose()
        {
            _pwm.Stop();
            _pwm.Dispose();
        }

    }
}
