using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;


public class DatabaseService
{

    private readonly AppDbContext _dbContext;
    public DatabaseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CurrentCondition currentCondition(string city){
        return _dbContext.CurrentConditions.Where(c => c.city == city).OrderByDescending(c => c.lastUpdateDate).FirstOrDefault();
    }

    public List<Forecast> forecast(string city){
        return _dbContext.Forecasts.Where(c => c.city == city && c.date >= DateTime.UtcNow.Date).ToList();
    }

    public List<Hourly> hourlies(){
        return _dbContext.Hourlies.ToList();
    }
    public async Task SaveCurrentConditionAsync(CurrentCondition newCurrentCondition)
    {
        try
        {
            newCurrentCondition.insertDate = DateTime.UtcNow;
            newCurrentCondition.lastUpdateDate = DateTime.UtcNow;
            _dbContext.CurrentConditions.AddRange(newCurrentCondition);
            await _dbContext.SaveChangesAsync();

            Console.WriteLine($"[Inserted] Saved condition: {newCurrentCondition.localObsDateTime}");            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Insert Error] {ex.Message}");
        }
    }

}