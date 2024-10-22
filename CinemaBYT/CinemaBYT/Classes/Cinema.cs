using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class Cinema
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ContactPhone { get; set; }
    public string OpeningHours { get; set; }
    [MinLength(1)]
    public List<Hall> Halls { get; set; }
}