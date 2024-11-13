using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using BeocreateRemote.Server.Services;

namespace BeocreateRemote.Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
#if DEBUG
            // builder.Services.AddSingleton<IRemoteController>(new SigmaTcpController("192.168.0.4"));
            builder.Services.AddSingleton<IRemoteController>(new ControllerMock());
            builder.Services.AddSingleton<ITemperatureReader>(new TemperatureReaderMock());
            builder.Services.AddSingleton<IFanController>(new FanControllerMock());
#else            
            builder.Services.AddSingleton<IRemoteController>(new SigmaTcpController("127.0.0.1"));
            builder.Services.AddSingleton<ITemperatureReader>();
            builder.Services.AddSingleton<IFanController>();
#endif            
            
            builder.Services.AddSingleton<FanInformation>();
            builder.Services.AddHostedService<TimedHostedService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
