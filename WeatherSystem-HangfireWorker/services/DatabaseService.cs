using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using WeatherSystem_HangfireWorker;

public class DatabaseService
{

    private readonly AppDbContext _dbContext;
    public DatabaseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveCurrentConditionAsync(CurrentCondition newCurrentCondition)
    {
        try
        {
            List<CurrentCondition> existingData = _dbContext.CurrentConditions.Where(currentCondition => 
                            currentCondition.localObsDateTime == newCurrentCondition.localObsDateTime && 
                            currentCondition.city == newCurrentCondition.city).ToList();
            if(existingData.AsEnumerable().Count() >0 ){
                // Ensure that all of previous coinciding items will be updated
                foreach(CurrentCondition updateCurrentCondition in existingData){
                    updateCurrentCondition.lastUpdateDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine($"[Job] Updated condition: {updateCurrentCondition.localObsDateTime}");
                }

            }
            else{
                newCurrentCondition.insertDate = DateTime.UtcNow;
                newCurrentCondition.lastUpdateDate = DateTime.UtcNow;
                _dbContext.CurrentConditions.AddRange(newCurrentCondition);
                await _dbContext.SaveChangesAsync();

                Console.WriteLine($"[Job] Saved condition: {newCurrentCondition.localObsDateTime}");
            }            

            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Job Error] {ex.Message}");
        }
    }

    public async Task SaveHourlyAsync(Hourly newHourly)
    {
        try
        {
            List<Hourly> existingData = _dbContext.Hourlies.Where(hourly => 
                            hourly.date == newHourly.date && 
                            hourly.city == newHourly.city &&
                            hourly.time == newHourly.time
                            ).ToList();
            if(existingData.AsEnumerable().Count() >0 ){
                // Ensure that all of previous coinciding items will be updated
                foreach(Hourly currentHourly in existingData){
                    if(currentHourly.lastUpdateDate.Date != DateTime.UtcNow.Date){
                        currentHourly.lastUpdateDate = DateTime.UtcNow;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            else{
                newHourly.insertDate = DateTime.UtcNow;
                newHourly.lastUpdateDate = DateTime.UtcNow;
                _dbContext.Hourlies.AddRange(newHourly);
                await _dbContext.SaveChangesAsync();
            }            

            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Job Error] {ex.Message}");
        }
    }

    public async Task SaveForecastAsync(Forecast newForecast)
    {
        try
        {
            List<Forecast> existingData = _dbContext.Forecasts.Where(forecast => 
                            forecast.date == newForecast.date && 
                            forecast.city == newForecast.city ).ToList();
            if(existingData.AsEnumerable().Count() >0 ){
                // Ensure that all of previous coinciding items will be updated
                foreach(Forecast currentForecast in existingData){
                    if(currentForecast.lastUpdateDate.Date != DateTime.UtcNow.Date){
                        currentForecast.lastUpdateDate = DateTime.UtcNow;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            else{
                newForecast.insertDate = DateTime.UtcNow;
                newForecast.lastUpdateDate = DateTime.UtcNow;
                _dbContext.Forecasts.AddRange(newForecast);
                await _dbContext.SaveChangesAsync();
            }               
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Job Error] {ex.Message}");
        }
    }


    public List<string> getCities(){
        List<string?> cities = _dbContext.CurrentConditions.Select(listedCity => listedCity.city).Distinct().ToList();
        if(cities != null){
            return cities;
        }
        return [];
    }
    
}
