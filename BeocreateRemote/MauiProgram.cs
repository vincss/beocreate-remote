using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using BeocreateRemote.Pages;
using Microsoft.Extensions.Logging;

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
                });

            builder.Services.AddSingleton<IRemoteController>(new SshController("192.168.0.4", "osmc", "osmc"));
            //builder.Services.AddSingleton<IRemoteController>(new SshController("192.168.0.4", "root", "hifiberry"));
            //builder.Services.AddSingleton<IRemoteController>(new OsmcMock());

            builder.Services.AddSingleton<AudioControlViewModel>();
            builder.Services.AddSingleton<AudioVolumePage>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();


#if DEBUG
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
