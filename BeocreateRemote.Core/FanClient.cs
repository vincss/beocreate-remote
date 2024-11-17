using System.Net.Http.Json;

namespace BeocreateRemote.Core;

public class FanClient(HttpClient httpClient)
{
    public async Task<FanInformation?> GetFanInformation() => await httpClient.GetFromJsonAsync<FanInformation>("http://localhost:5000/fan");
}