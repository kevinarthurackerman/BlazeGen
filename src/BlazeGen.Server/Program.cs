using BlazeGen.Server.Pages;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x
    .WithOrigins("https://localhost:7105/")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.AddWeather();

app.Run();