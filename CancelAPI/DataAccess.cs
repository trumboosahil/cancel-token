using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CancelAPI
{
    public class DataAccess
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public async Task<List<WeatherForecast>> GetData(CancellationToken token)
        {
            try
            {
                
                await Task.Delay(10000, token);
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();
            }
            catch (TaskCanceledException ex)
            {
                 token.ThrowIfCancellationRequested();
            }
            return null;
        }
    }
}
