using Microsoft.Extensions.Logging;

namespace BeocreateRemote.Core
{
    public class TemperatureReader : ITemperatureReader
    {
        const string TemperatureFile = "/sys/class/thermal/thermal_zone0/temp";

        public int Read() {
            var temp = File.ReadAllText(TemperatureFile);
            return (int) float.Parse(temp) / 1000;
        }
    }

    public interface ITemperatureReader 
    {
        public int Read();
    }
}
