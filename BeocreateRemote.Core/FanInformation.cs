namespace BeocreateRemote.Core;

public class FanInformation
{
    public int Temperature { get; set; }
    public int Speed { get; set; }

    public static readonly FanInformation FanError = new() { Speed = -99, Temperature = - 99 };
}