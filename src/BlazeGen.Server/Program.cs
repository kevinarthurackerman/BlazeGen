using BlazeGen.Server.Data;
using BlazeGen.Server.Pages;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(x => x.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default));

builder.Services.AddScoped<DbContext>();
builder.Services.AddWeather();

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors(x => x
    .WithOrigins("https://localhost:7105/")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.AddWeather();

app.Run();