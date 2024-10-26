using CinemaBYT.Classes;
using System.Xml.Linq;
LoadInfo loadInfo = new LoadInfo();
string filePath = "text.xml";

loadInfo.LoadFromXml(filePath);
Console.WriteLine($"Cinema Name: {loadInfo.CinemaName}");
Console.WriteLine($"City: {loadInfo.City}");
Console.WriteLine($"Country: {loadInfo.Country}");
Console.WriteLine($"Movie Name: {loadInfo.MovieName}");
Console.WriteLine($"Release Date: {loadInfo.ReleaseDate}");
Console.WriteLine($"Age Rating: {loadInfo.AgeRating}");
