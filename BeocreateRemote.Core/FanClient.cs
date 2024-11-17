using System.Net.Http.Json;

namespace BeocreateRemote.Core;

public class FanClient(HttpClient httpClient, Configuration configuration)
{
    public async Task<FanInformation?> GetFanInformation()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<FanInformation>($"{configuration.BeocreateRemoteServerAddress}/fan");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return FanInformation.FanError;
    }
}