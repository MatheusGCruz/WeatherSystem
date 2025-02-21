using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherSystem_HangfireWorker;

public class JobService
{
    private readonly ApiService _apiService;
    private readonly DatabaseService _databaseService;

    public JobService(ApiService apiService, DatabaseService databaseService)
    {
        _apiService = apiService;
        _databaseService = databaseService;
    }

    public async Task FetchAndSaveDataAsync(){
        try
        {
            List<string> cities = _databaseService.getCities();

            foreach(string city in cities){
                await FetchAndSaveDataAsyncByCity(city);
            }           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Job Error] {ex.Message}");
        }
    }

    public async Task FetchAndSaveDataAsyncByCity(string city)
    {
        try
        {
            string data = await _apiService.FetchDataAsync(city);

            WttrObject wttrObject = JsonConvert.DeserializeObject<WttrObject>(data);
            var currentCondition = wttrObject.current_Condition.FirstOrDefault() ;

            if(currentCondition != null){
                currentCondition.city = wttrObject.nearest_area.FirstOrDefault().areaName.FirstOrDefault().value;
                currentCondition.country = wttrObject.nearest_area.FirstOrDefault().country.FirstOrDefault().value;
                await _databaseService.SaveCurrentConditionAsync(currentCondition);
                Console.WriteLine($"[Job] Current valid condition: {currentCondition.localObsDateTime}");
            }  

            var weatherList = wttrObject.weather;

            if(weatherList != null){
                foreach(WeatherItem weather in weatherList){
                    foreach(Hourly hourly in weather.hourly){
                        hourly.avgtempC = weather.avgtempC;
                        hourly.avgtempF = weather.avgtempF;
                        hourly.date = weather.date;
                        hourly.maxtempC = weather.maxtempC;
                        hourly.maxtempF = weather.maxtempF;
                        hourly.mintempC = weather.mintempC;
                        hourly.mintempF = weather.mintempF;
                        hourly.sunHour = weather.sunHour;
                        hourly.totalSnow_cm = weather.totalSnow_cm;
                        hourly.city = city;

                        await _databaseService.SaveHourlyAsync(hourly);
                    }   

                    Forecast newForecast = new Forecast();
                    newForecast.avgtempC = weather.avgtempC;
                    newForecast.avgtempF = weather.avgtempF;
                    newForecast.city = city;
                    newForecast.date = weather.date;
                    newForecast.maxtempC = weather.maxtempC;
                    newForecast.maxtempF = weather.maxtempF;
                    newForecast.mintempC = weather.mintempC;
                    newForecast.mintempF = weather.mintempF;
                    newForecast.sunHour = weather.sunHour;
                    newForecast.totalSnow_cm = weather.totalSnow_cm;
                    newForecast.uvIndex = weather.uvIndex;

                    await _databaseService.SaveForecastAsync(newForecast);                                     
                }
            }





            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Job Error] {ex.Message}");
        }
    }
}
