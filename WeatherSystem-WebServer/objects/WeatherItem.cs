using System;
using System.ComponentModel.DataAnnotations;

public class WeatherItem
{
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int temperatureC {get; set;} = 0;
    public int temperatureF {get; set;} = 0;
    public int humidity {get; set;} = 0;
    public int pressure {get; set; } = 0;
    public int sourceId {get; set;} = 0;   

}
