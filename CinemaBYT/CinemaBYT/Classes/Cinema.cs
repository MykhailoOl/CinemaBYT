using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using CinemaBYT.Exceptions;

public class Cinema
{
    [DisallowNull]
    public string Name { get; set; }
    [DisallowNull]
    public string City { get; set; }
    [DisallowNull]
    public string Country { get; set; }
    [DisallowNull]
    public string ContactPhone { get; set; }
    [DisallowNull]
    public string OpeningHours { get; set; }
    [MinLength(1)]
    public List<Hall> Halls { get; set; }

    public Cinema(string name, string city, string country, string contactPhone, string openingHours)
    {
        Name = name;
        City = city;
        Country = country;
        ContactPhone = contactPhone;
        OpeningHours = openingHours;
        Halls = new List<Hall>();
    }
    public List<Hall> GetAvailableHalls()
    {
        if (Halls == null)
        {
            throw new CinemaException("The halls list is not initialized.");
        }
        List<Hall> availableHalls = new List<Hall>();
        foreach (var hall in Halls)
        {
            if (hall.HasAvailableSeats())
            {
                availableHalls.Add(hall);
            }
        }
        if (availableHalls.Count == 0)
        {
            throw new CinemaException("No available halls with seats found.");
        }
        return availableHalls;
    }
}