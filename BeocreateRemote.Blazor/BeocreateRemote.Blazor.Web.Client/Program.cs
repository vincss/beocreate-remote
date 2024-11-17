using BeocreateRemote.Blazor.Shared.Services;
using BeocreateRemote.Blazor.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the BeocreateRemote.Blazor.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddBeocreateServices();

await builder.Build().RunAsync();
