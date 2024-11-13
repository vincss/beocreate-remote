using BeocreateRemote.Core;

namespace BeocreateRemote.Server.Services;

public class TimedHostedService(
    ILogger<TimedHostedService> logger,
    ITemperatureReader temperatureReader,
    IFanController fanController,
    FanInformation fanInformation)
    : IHostedService, IDisposable
{
    private Timer? timer;
    private const int TimerIntervalSeconds = 2;
    
    public Task StartAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Timed Hosted Service running.");

        timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(TimerIntervalSeconds));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        fanInformation.Temperature = temperatureReader.Read();
        var speed = FanHelper.GetSpeedFromTemperature(fanInformation.Temperature);
        fanController.Set(speed);
        fanInformation.Speed = (int)(speed * 100);

        logger.LogInformation("Temperature {Temperature}°c => Fan {Speed}%", fanInformation.Temperature, fanInformation.Speed );
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Timed Hosted Service is stopping.");

        timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        fanController.Dispose();
        timer?.Dispose();
        logger.LogInformation("Timed Hosted Disposed.");
    }
}