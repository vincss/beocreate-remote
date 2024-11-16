using BeocreateRemote.Blazor.Shared.Services;
using BeocreateRemote.Blazor.Web.Components;
using BeocreateRemote.Blazor.Web.Services;
using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the BeocreateRemote.Blazor.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddSingleton<IRemoteController, ControllerMock>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(BeocreateRemote.Blazor.Shared._Imports).Assembly,
        typeof(BeocreateRemote.Blazor.Web.Client._Imports).Assembly);

app.Run();
