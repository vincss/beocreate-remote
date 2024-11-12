using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeocreateRemote.Core
{
    public class TemperatureReader
    {
        const string TemperatureFile = "/sys/class/thermal/thermal_zone0/temp";

        private readonly ILogger<TemperatureReader> _logger;

        public TemperatureReader(ILogger<TemperatureReader> logger)
        {
            _logger = logger;
        }

        public int Read() {
            var temp = File.ReadAllText(TemperatureFile);
            return (int) float.Parse(temp) / 1000;
        }
    }
}
