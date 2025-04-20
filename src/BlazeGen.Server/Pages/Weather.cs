using System.Text.Json.Serialization;

namespace BlazeGen.Server.Pages
{
    public static class Weather
    {
        public static IEndpointRouteBuilder AddWeather(this IEndpointRouteBuilder builder)
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();

            var todosApi = builder.MapGroup("/weather");
            todosApi.MapGet("/forecasts", () => forecasts);
            todosApi.MapGet("/forecasts/{id}", (int id) =>
                forecasts.FirstOrDefault(a => a.Id == id) is { } todo
                    ? Results.Ok(todo)
                    : Results.NotFound());

            return builder;
        }
    }

    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    [JsonSerializable(typeof(WeatherForecast[]))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
