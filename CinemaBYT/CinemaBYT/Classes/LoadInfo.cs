using System;
using System.Xml;

namespace CinemaBYT.Classes
{
    public class LoadInfo
    {
        public string CinemaName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MovieName { get; set; }
        public string ReleaseDate { get; set; }
        public string AgeRating { get; set; }

        public void LoadFromXml(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode cinemaNode = doc.SelectSingleNode("/CinemaData/Cinema");
            if (cinemaNode != null)
            {
                CinemaName = cinemaNode["Name"]?.InnerText;
                City = cinemaNode["City"]?.InnerText;
                Country = cinemaNode["Country"]?.InnerText;
            }

            XmlNode movieNode = doc.SelectSingleNode("/CinemaData/Movie");
            if (movieNode != null)
            {
                MovieName = movieNode["Name"]?.InnerText;
                ReleaseDate = movieNode["ReleaseDate"]?.InnerText;
                AgeRating = movieNode["AgeRating"]?.InnerText;
            }
        }
    }
}
