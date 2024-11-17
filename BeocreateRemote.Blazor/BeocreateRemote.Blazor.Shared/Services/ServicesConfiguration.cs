using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using Microsoft.Extensions.DependencyInjection;

namespace BeocreateRemote.Blazor.Shared.Services;

public static class ServicesConfiguration
{
    public static void AddBeocreateServices(this IServiceCollection services)
    {
        var configuration = new RemoteConfiguration() { SigmaTcpAddress = "192.168.0.4", BeocreateRemoteServerAddress = "http://192.168.0.4:5000"};
        IRemoteController remoteController = new SigmaTcpController(configuration.SigmaTcpAddress);
#if DEBUG
        remoteController = new ControllerMock();
#endif
        services.AddSingleton(remoteController);
        services.AddSingleton<HttpClient>();
        services.AddSingleton<FanClient>();
    }
}