using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using jappo;
using jappo.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IStorageService, LocalStorageService>();
builder.Services.AddSingleton<ICharacterRepository, CharacterRepository>();
builder.Services.AddSingleton<ISrsService, SrsService>();

var host = builder.Build();

// Pre-load character data before rendering
var repo = host.Services.GetRequiredService<ICharacterRepository>();
await repo.InitializeAsync();

await host.RunAsync();
