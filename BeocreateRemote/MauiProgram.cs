﻿using BeocreateRemote.Core;
using BeocreateRemote.ViewModel;
using Microsoft.Extensions.Logging;
using Renci.SshNet;

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

            builder.Services.AddSingleton<SshController>(new SshController("192.168.0.4", "root", "hifiberry"));
            

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();


#if DEBUG
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
