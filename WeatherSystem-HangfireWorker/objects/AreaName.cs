using System;
using System.ComponentModel.DataAnnotations;

public class AreaName
{
    private AreaName areaName;

    public AreaName(AreaName areaName)
    {
        this.areaName = areaName;
    }

    public string value {get; set;}
    
}