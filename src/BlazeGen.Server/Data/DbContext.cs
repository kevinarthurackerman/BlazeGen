namespace BlazeGen.Server.Data
{
    public class DbContext
    {
        private static readonly IEnumerable<WeatherForecast> _forecasts;

        static DbContext()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            _forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })
            .ToArray();
        }

        public IEnumerable<WeatherForecast> WeatherForecasts { get; set; } = _forecasts;
    }
}
