using BeocreateRemote.Helper;
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
                })
                ;

            builder.Services.AddSingleton(new ControllerContainer());

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
