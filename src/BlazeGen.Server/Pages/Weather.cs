using BlazeGen.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace BlazeGen.Server.Pages
{
    public static class Weather
    {
        public static IServiceCollection AddWeather(this IServiceCollection services)
        {
            services.AddSingleton<TemperatureConverter>();
            services.AddSingleton<WeatherMapper>();

            return services;
        }

        public static IEndpointRouteBuilder AddWeather(this IEndpointRouteBuilder builder)
        {
            var todosApi = builder.MapGroup("/weather");

            todosApi.MapGet("/forecasts", ([FromServices] DbContext dbContext, [FromServices] WeatherMapper mapper) =>
                Results.Ok(dbContext.WeatherForecasts.ToArray().Select(mapper.ToApi)));

            todosApi.MapGet("/forecasts/{id}", ([FromRoute] int id, [FromServices] DbContext dbContext, [FromServices] WeatherMapper mapper) =>
                dbContext.WeatherForecasts.FirstOrDefault(a => a.Id == id) is { } todo
                    ? Results.Ok(mapper.ToApi(todo))
                    : Results.NotFound());

            return builder;
        }
    }

    public record class WeatherForecast(
        int Id,
        DateOnly Date,
        int TemperatureC,
        string? Summary,
        int TemperatureF);

    [JsonSerializable(typeof(WeatherForecast))]
    [JsonSerializable(typeof(IEnumerable<WeatherForecast>))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }

    [Mapper]
    public partial class WeatherMapper(TemperatureConverter temperatureConverter)
    {
        private readonly TemperatureConverter temperatureConverter = temperatureConverter;

        [MapPropertyFromSource(nameof(WeatherForecast.TemperatureF), Use = nameof(MapTemperatureF))]
        public partial WeatherForecast ToApi(Data.WeatherForecast dto);

        private int MapTemperatureF(Data.WeatherForecast dto)
            => temperatureConverter.FahrenheitToCelsius(dto.TemperatureC);
    }

    public class TemperatureConverter
    {
        public int CelsiusToFahrenheit(int celsius) => (int)(32 + (celsius * 9.0 / 5.0));

        public int FahrenheitToCelsius(int fahrenheit) => (int)((fahrenheit - 32) * (5.0/9.0));
    }
}
