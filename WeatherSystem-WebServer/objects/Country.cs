using System;
using System.ComponentModel.DataAnnotations;

public class Country
{
    private Country country;

    public Country(Country country)
    {
        this.country = country;
    }
    
    public string value {get; set;}
}