namespace BeocreateRemote.Core
{
    public static class FanConfiguration
    {
        public static double MinSpeed = 0.60;
        public static double MaxSpeed = 1.00;

        public static int MinTemp = 55;
        public static int MaxTemp = 70;
    }

    public class FanHelper
    {
        public static double GetSpeedFromTemperature(int temperature)
        {
            if (temperature < FanConfiguration.MinTemp)
            {
                return FanConfiguration.MinSpeed;
            }
            if (temperature > FanConfiguration.MaxTemp)
            {
                return FanConfiguration.MaxSpeed;
            }

            var temperatureInterval = FanConfiguration.MaxTemp - FanConfiguration.MinTemp;
            var speedInterval = FanConfiguration.MaxSpeed - FanConfiguration.MinSpeed;
            var temperatureOffset = temperature - FanConfiguration.MinTemp;

            return (((double)temperatureOffset / (double)temperatureInterval) * speedInterval) + FanConfiguration.MinSpeed;
        }
    }
}
