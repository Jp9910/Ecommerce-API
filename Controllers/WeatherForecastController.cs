using Microsoft.AspNetCore.Mvc;

namespace webapicsharp.Controllers;

// [ApiController] enables opinionated behaviors that make it easier to build web APIs. Some behaviors 
// include parameter source inference, attribute routing as a requirement, and model validation error-handling enhancements.
[ApiController]

/*
 * [Route] defines the routing pattern [controller]. The [controller] token is replaced by the controller's 
 * name (case-insensitive, without the Controller suffix). This controller handles requests to https://localhost:{PORT}/weatherforecast.
 * The route might contain static strings, as in api/[controller]. 
 * In this example, this controller would handle a request to https://localhost:{PORT}/api/weatherforecast.
 */
[Route("api/[controller]")]

/*
 * Don't create a web API controller by deriving from the Controller class. Controller derives 
 * from ControllerBase and adds support for views, so >> it's for handling webpages, not web API requests <<.
 */
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
