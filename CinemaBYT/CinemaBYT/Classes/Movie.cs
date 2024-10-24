using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;

public class Movie
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int AgeRating { get; set; }
    public List<string> ListOfGenres { get; set; }

    public Movie(string name, DateTime releaseDate, int ageRating, List<string> listOfGenres)
    {
        Name = name;
        ReleaseDate = releaseDate;
        AgeRating = ageRating;
        ListOfGenres = listOfGenres;
    }
    
    public bool IsSuitableForAge(int age)
    {
        if (age < 0)
        {
            throw new MovieException("Age cannot be negative.");
        }

        if (age > 120)
        {
            throw new MovieException("Age is too high to be valid.");
        }

        return age >= AgeRating;
    }
}

