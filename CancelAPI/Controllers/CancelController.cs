using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CancelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CancelController : ControllerBase
    {
        private readonly ILogger<CancelController> _logger;

        private static readonly string[] Summaries = new[]
         {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public CancelController(ILogger<CancelController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken token)
        {
            try
            {
                Console.WriteLine("CancellationToken");
                await Task.Delay(10000,token);
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
            return null;

        }
    }
}
