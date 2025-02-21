using System;
using System.ComponentModel.DataAnnotations;

public class SourceItem
{
    public int Id { get; set; }

    [Required]
    public string ItemName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int type {get; set;} = 0;
    public int latitude {get; set;} = 0;
    public int longitud {get; set;} = 0;
    public int status {get; set; } = 0;
}