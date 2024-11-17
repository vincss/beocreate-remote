using BeocreateRemote.Blazor.Services;
using BeocreateRemote.Blazor.Shared.Services;
using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using Microsoft.Extensions.Logging;

namespace BeocreateRemote.Blazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the BeocreateRemote.Blazor.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddBeocreateServices();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
