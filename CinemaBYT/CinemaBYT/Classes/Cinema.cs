using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT;
using CinemaBYT.Exceptions;
using System.Collections;

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

    [MinLength(1)] private List<Hall> Halls;

    public List<Hall> halls
    {
        get => Halls;
        set
        {
            if (value == null || value.Count < 1)
            {
                throw new ArgumentException("Halls must contain at least one hall.", nameof(Halls));
            }
            halls = value;
        }
    }
    public void addHall(Hall hall)
    {
        if (!Halls.Contains(hall))
        {
            Halls.Add(hall);
            hall.addCinema(this); 
        }
    }
    public void deleteHall(Hall hall) {
        if (!Halls.Contains(hall))
        {
            throw new InvalidOperationException("The specified hall does not belong to this cinema.");
        }
        Halls.Remove(hall);
        hall.deleteCinema();
    }
    public void deleteCinema()
    {
        
        foreach (var hall in Halls.ToList()) 
        {
            hall.deleteCinema();
        }

        Halls.Clear(); 
    }
    public void applyHallToOtherCinema(Cinema cinema, Hall hall)
    {
        if (!this.Halls.Contains(hall)) {
            throw new InvalidOperationException("The specified hall does not belong to this cinema.");
        }
        Halls.Remove(hall);
        cinema.addHall(hall);
    }
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
    public override bool Equals(object obj)
    {
        if (obj is Cinema otherCinema)
        {
            return Name == otherCinema.Name &&
                   City == otherCinema.City &&
                   Country == otherCinema.Country &&
                   ContactPhone == otherCinema.ContactPhone &&
                   OpeningHours == otherCinema.OpeningHours &&
                   Halls.SequenceEqual(otherCinema.Halls);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, City, Country, ContactPhone, OpeningHours);
    }
}