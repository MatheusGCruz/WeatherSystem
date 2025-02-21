using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WeatherSystem_WebServer.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowAnyOrigin")] // Apply CORS policy here
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DatabaseService _databaseService;
    private readonly ApiService _apiService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DatabaseService databaseService, ApiService apiService)
    {
        _logger = logger;
        _databaseService = databaseService;
        _apiService = apiService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public CurrentCondition Get()
    {      
        CurrentCondition currentCondition =_databaseService.currentCondition("uberlandia");
        return currentCondition;

    }

    
    [HttpGet("condition/{city}")]
    public async Task<CurrentCondition> GetCurrentWeatherAsync(string city)
    {
        CurrentCondition searchCondition =_databaseService.currentCondition(city);
        if(searchCondition != null){
            return searchCondition;
        }

        else{
            string data = await _apiService.FetchDataAsync(city);

            WttrObject wttrObject = JsonConvert.DeserializeObject<WttrObject>(data);
            var currentCondition = wttrObject.current_Condition.OrderByDescending(c => c.lastUpdateDate).FirstOrDefault() ;

            if(currentCondition != null){
                currentCondition.city = wttrObject.nearest_area.FirstOrDefault().areaName.FirstOrDefault().value;
                currentCondition.country = wttrObject.nearest_area.FirstOrDefault().country.FirstOrDefault().value;
                _databaseService.SaveCurrentConditionAsync(currentCondition);
                return currentCondition;
            }  
        }
        return new CurrentCondition();
    }

    [HttpGet("forecast/{city}")]
    public List<Forecast> GetWeatherForecast(string city)
    {
        List<Forecast> forecasts = _databaseService.forecast(city);
        if(forecasts != null){
            return forecasts;
        }

        return [];
    }
}
