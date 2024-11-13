using System.Device.Pwm.Drivers;

namespace BeocreateRemote.Core
{
    public class FanController : IFanController
    {
        const int Pin = 12;
        private readonly SoftwarePwmChannel _pwm;

        public FanController()
        {
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

    public interface IFanController : IDisposable
    {
        public void Set(double speed);
    }
}
