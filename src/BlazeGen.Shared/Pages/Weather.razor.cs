using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazeGen.Shared.Pages
{
    public partial class Weather(HttpClient client) : ComponentBase
    {
        private readonly HttpClient client = client;

        private WeatherForecast[]? forecasts;
        
        protected override async Task OnInitializedAsync()
        {
            forecasts = await client.GetFromJsonAsync<WeatherForecast[]>("weather/forecasts");
        }

        private class WeatherForecast
        {
            public int Id { get; set; }
            public DateOnly Date { get; set; }
            public int TemperatureC { get; set; }
            public string? Summary { get; set; }
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
