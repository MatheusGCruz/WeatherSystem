public class Forecast{
    public int Id { get; set; }
    public DateTime? date { get; set; }
    public int avgtempC { get; set; }
    public int avgtempF { get; set; }
    public int maxtempC { get; set; }
    public int maxtempF { get; set; }
    public int mintempC { get; set; }
    public int mintempF { get; set; }
    public float sunHour { get; set; }
    public float totalSnow_cm { get; set; }
    public int uvIndex { get; set; }
    public DateTime insertDate { get; set; }
    public DateTime lastUpdateDate { get; set; }
    public string? city {get; set;}

    public static explicit operator Forecast(WeatherItem v)
    {
        throw new NotImplementedException();
    }
}