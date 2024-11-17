using BeocreateRemote.Blazor.Shared.Services;
using BeocreateRemote.Blazor.Web.Client.Services;
using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the BeocreateRemote.Blazor.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddSingleton<IRemoteController>(new ControllerMock());
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<FanClient>();

await builder.Build().RunAsync();
