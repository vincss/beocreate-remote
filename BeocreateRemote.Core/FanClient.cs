using System.Net.Http.Json;

namespace BeocreateRemote.Core;

public class FanClient(HttpClient httpClient, RemoteConfiguration remoteConfiguration)
{
    public async Task<FanInformation?> GetFanInformation()
    {
        try
        {
            return await httpClient.GetFromJsonAsync<FanInformation>($"{remoteConfiguration.BeocreateRemoteServerAddress}/fan");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return FanInformation.FanError;
    }
}