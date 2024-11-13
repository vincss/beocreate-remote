namespace BeocreateRemote.Core.mock;

public class TemperatureReaderMock : ITemperatureReader
{
    private readonly Random _random = new();

    public int Read()
    {
        return _random.Next(FanConfiguration.MinTemp, FanConfiguration.MaxTemp);
    }
}