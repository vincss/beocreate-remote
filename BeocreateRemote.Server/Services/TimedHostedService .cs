using BeocreateRemote.Core;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TimedHostedService> _logger;
    private readonly TemperatureReader _temperatureReader;
    private readonly FanController _fanController;
    private Timer? _timer = null;
    private int TimerIntervalSeconds = 2;


    public TimedHostedService(ILogger<TimedHostedService> logger, TemperatureReader temperatureReader, FanController fanController)
    {
        _logger = logger;
        _temperatureReader = temperatureReader;
        _fanController = fanController;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(TimerIntervalSeconds));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        var temperature = _temperatureReader.Read();
        var speed = FanHelper.GetSpeedFromTemperature(temperature);
        _fanController.Set(speed);
        _logger.LogInformation("Temperature {temperature}°c => Fan {speed}%", temperature, (int)(speed * 100));
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _fanController.Dispose();
        _timer?.Dispose();
        _logger.LogInformation("Timed Hosted Disposed.");
    }
}