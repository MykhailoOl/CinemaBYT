using System.Xml.Linq;
string filePath = "text.xml";

if (!File.Exists(filePath))
{
    Console.WriteLine($"File '{filePath}' not found.");
    return;
}

try
{
    List<Cinema> cinemas = Cinema.LoadFromXml(filePath);
    foreach (var cinema in cinemas)
    {
        Console.WriteLine($"Cinema: {cinema.Name}, City: {cinema.City}, Country: {cinema.Country}");
        Console.WriteLine("  Halls:");

        foreach (var hall in cinema.Halls)
        {
            Console.WriteLine($"    Hall {hall.HallNumber}, Number of Seats: {hall.NumberOfSeats}");
            Console.WriteLine("      Seats:");

            foreach (var seat in hall.Seats)
            {
                Console.WriteLine($"        Seat {seat.SeatNo}, VIP: {seat.IsVIP}, Available: {seat.IsAvailable}");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
    