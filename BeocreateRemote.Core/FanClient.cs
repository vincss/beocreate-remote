using System.Net.Http.Json;

namespace BeocreateRemote.Core;

public class FanClient(HttpClient httpClient)
{
    public async Task<FanInformation?> GetFanInformation()
    {

        try
        {
            return await httpClient.GetFromJsonAsync<FanInformation>("http://192.168.0.17:5000/fan");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return FanInformation.FanError;
    }
}