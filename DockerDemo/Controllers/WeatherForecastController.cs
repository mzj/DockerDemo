using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DockerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private static readonly HttpClient _httpClient = new HttpClient();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {

            List<WeatherForecast> reservationList = new List<WeatherForecast>();
            string apiResponse = string.Empty;

            using (var cts = new CancellationTokenSource(new TimeSpan(0, 0, 10)))
            {
                using (var response = await _httpClient.GetAsync("http://slowwly.robertomurray.co.uk/delay/500/url/https://www.google.co.uk", cancellationToken: cts.Token).ConfigureAwait(false))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            if (!string.IsNullOrEmpty(apiResponse))
            {
                //reservationList = JsonConvert.DeserializeObject<List<WeatherForecast>>(apiResponse);
                reservationList = new List<WeatherForecast> { new WeatherForecast { Date = DateTime.Now, Summary = "fdf", TemperatureC = 33 } };
            }
            return reservationList;
        }
    }
}
