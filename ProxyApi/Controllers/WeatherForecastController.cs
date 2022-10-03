using Microsoft.AspNetCore.Mvc;

namespace ProxyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly string _weatherApiUrl;
    private readonly IHttpClientFactory httpClientFactory;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _weatherApiUrl = configuration.GetValue<string>("AppSettings:WeatherApiUrl");
        this.httpClientFactory = httpClientFactory;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<Proxies.WeatherApi.WeatherForecast>> Get()
    {
        _logger.LogInformation("_weatherApiUrl:" + _weatherApiUrl);
        var client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_weatherApiUrl);
        var data = await client.GetFromJsonAsync<IEnumerable<Proxies.WeatherApi.WeatherForecast>>("/WeatherForecast");
        return data ?? Enumerable.Empty<Proxies.WeatherApi.WeatherForecast>();
    }
}
