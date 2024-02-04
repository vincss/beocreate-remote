using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using BeocreateRemote.Helper;
using BeocreateRemote.Model;
using BeocreateRemote.Pages;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace BeocreateRemote
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                ;

            // ToDo change injection depending on factory ?
            builder.Services.AddSingleton<ControllerContainer>(new ControllerContainer());
            //builder.Services.AddSingleton<IRemoteController>(new SshController("192.168.0.4", "root", "hifiberry"));
            //builder.Services.AddSingleton<IRemoteController>(new OsmcMock());

            builder.Services.AddSingleton<ConfigurationViewModel>();
            builder.Services.AddSingleton<ConfigurationPage>();

            builder.Services.AddSingleton<AudioControlViewModel>();
            builder.Services.AddSingleton<AudioControlPage>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
