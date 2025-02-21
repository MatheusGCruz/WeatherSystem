using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CurrentCondition> CurrentConditions { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<DataItem> DataItems { get; set; }
    public DbSet<Hourly> Hourlies { get; set; }
    public DbSet<WeatherItem> WeatherItems { get; set; }
    public DbSet<Forecast> Forecasts { get; set; }


}
