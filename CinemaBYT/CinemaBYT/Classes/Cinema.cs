using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using CinemaBYT.Exceptions;

public class Cinema
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ContactPhone { get; set; }
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
    public static List<Cinema> LoadFromXml(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        var cinemas = doc.Descendants("cinema")
            .Select(c => new Cinema(
                c.Element("name").Value,
                c.Element("city").Value,
                c.Element("country").Value,
                c.Element("contactPhone").Value,
                c.Element("openingHours").Value
            )).ToList();

        var halls = doc.Descendants("hall")
            .Select(h => new Hall(
                int.Parse(h.Element("hallNumber").Value),
                int.Parse(h.Element("numberOfSeats").Value),
                h.Descendants("seat")
                    .Select(s => new Seat(
                        int.Parse(s.Element("seatNo").Value),
                        s.Element("isVIP").Value == "1",
                        s.Element("isAvailable").Value == "1"
                    )).ToList()
            )).ToList();


        foreach (var cinema in cinemas)
        {
            var cinemaHalls = halls.Where(h => true).ToList();
            cinema.Halls.AddRange(cinemaHalls);

            foreach (var hall in cinemaHalls)
            {
                hall.SetCinema(cinema);  
            }
        }

        return cinemas;
    }
}