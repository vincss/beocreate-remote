using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using Microsoft.Extensions.DependencyInjection;

namespace BeocreateRemote.Blazor.Shared.Services;

public static class ServicesConfiguration
{
    public static void AddBeocreateServices(this IServiceCollection services)
    {
        services.AddSingleton<IRemoteController>(new ControllerMock());
        services.AddSingleton<HttpClient>();
        services.AddSingleton<FanClient>();

    }
}