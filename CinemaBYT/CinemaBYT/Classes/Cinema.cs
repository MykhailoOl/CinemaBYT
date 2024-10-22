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

    public Cinema(string name, string city, string country, string contactPhone, string openingHours, List<Hall> halls)
    {
        Name = name;
        City = city;
        Country = country;
        ContactPhone = contactPhone;
        OpeningHours = openingHours;
        Halls = halls;
    }
    public List<Hall> GetAvailableHalls()
    {
        List<Hall> availableHalls = new List<Hall>();
        foreach (var hall in Halls)
        {
            if (hall.HasAvailableSeats())
            {
                availableHalls.Add(hall);
            }
        }
        return availableHalls;
    }
}