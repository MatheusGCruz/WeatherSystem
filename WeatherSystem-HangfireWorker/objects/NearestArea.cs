using System;
using System.ComponentModel.DataAnnotations;

public class NearestArea
{
    private NearestArea nearestArea;

    public NearestArea(NearestArea nearestArea)
    {
        this.nearestArea = nearestArea;
    }

    public List<AreaName> areaName {get; set;}
    public List<Country>  country {get; set;}
    
}