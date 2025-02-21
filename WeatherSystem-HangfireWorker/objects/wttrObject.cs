using System;
using System.ComponentModel.DataAnnotations;

public class WttrObject
{
    private WttrObject wttrObject;

    public WttrObject(WttrObject wttrObject)
    {
        this.wttrObject = wttrObject;
    }

    public List<CurrentCondition> current_Condition {get; set;}
    public List<NearestArea> nearest_area {get; set;}
    //public string request {get; set;}
    public List<WeatherItem> weather {get; set;}

}