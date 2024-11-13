namespace BeocreateRemote.Core.mock;

public class FanControllerMock : IFanController
{
    public void Dispose()
    {
    }

    public void Set(double speed)
    {
        Console.WriteLine($"FanControllerMock Speed: {speed}");
    }
}