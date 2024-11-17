using BeocreateRemote.Blazor.Shared.Services;
using BeocreateRemote.Blazor.Web.Components;
using BeocreateRemote.Blazor.Web.Services;
using BeocreateRemote.Core;
using BeocreateRemote.Core.mock;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCors(o =>
    o.AddPolicy("AllowCors", configure => { configure.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

// Add device-specific services used by the BeocreateRemote.Blazor.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddSingleton<IRemoteController>(new ControllerMock());
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<FanClient>();

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

app.UseCors("AllowCors");

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(BeocreateRemote.Blazor.Shared._Imports).Assembly,
        typeof(BeocreateRemote.Blazor.Web.Client._Imports).Assembly);

app.Run();