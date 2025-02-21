using System;
using System.ComponentModel.DataAnnotations;

public class CurrentCondition
{
    public int Id { get; set; }
    public int FeelsLikeC { get; set; }
    public int FeelsLikeF { get; set; }
    public int cloudcover { get; set; }
    public int humidity { get; set; }
    public DateTime localObsDateTime { get; set; }
    public DateTime observation_time { get; set; }
    public float precipInches { get; set; }
    public float precipMM { get; set; }
    public int pressure { get; set; }
    public int pressureInches { get; set; }
    public int temp_C { get; set; }
    public int temp_F { get; set; }
    public int uvIndex { get; set; }
    public int visibility { get; set; }
    public int visibilityMiles { get; set; }
    public int weatherCode { get; set; }
    //public WeatherDesc weatherDesc { get; set; }
    //public WeatherIconUrl weatherIconUrl { get; set; }
    public string winddir16Point { get; set; }
    public int winddirDegree { get; set; }
    public int windspeedKmph { get; set; }
    public int windspeedMiles { get; set; }
    public DateTime? insertDate { get; set; }
    public DateTime? lastUpdateDate { get; set; }
    public string? city {get; set;}
    public string? country {get; set;}

}