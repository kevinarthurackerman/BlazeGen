using BlazeGen.Shared.Services;
using BlazeGen.Wasm;
using BlazeGen.Wasm.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("", x => x.BaseAddress = new Uri("http://localhost:5027"));

builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
