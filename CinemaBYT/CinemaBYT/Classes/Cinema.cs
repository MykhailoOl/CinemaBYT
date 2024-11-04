using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using CinemaBYT;
using CinemaBYT.Exceptions;

public class Cinema
{
    private string _name;
    private string _city;
    private string _country;
    private string _contactPhone;
    private string _openingHours;

    [DisallowNull]
    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(Name), "Name cannot be null.");
    }
    
    [DisallowNull]
    public string City
    {
        get => _city;
        set => _city = value ?? throw new ArgumentNullException(nameof(City), "City cannot be null.");
    }
    
    [DisallowNull]
    public string Country
    {
        get => _country;
        set => _country = value ?? throw new ArgumentNullException(nameof(Country), "Country cannot be null.");
    }
    
    [DisallowNull]
    public string ContactPhone
    {
        get => _contactPhone;
        set => _contactPhone = value ?? throw new ArgumentNullException(nameof(ContactPhone), "Contact phone cannot be null.");
    }
    
    [DisallowNull]
    public string OpeningHours
    {
        get => _openingHours;
        set => _openingHours = value ?? throw new ArgumentNullException(nameof(OpeningHours), "Opening hours cannot be null.");
    }

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