using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class Movie
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int AgeRating { get; set; }
    [MinLength(1)]
    public List<string> ListOfGenres { get; set; }

    public Movie(string name, DateTime releaseDate, int ageRating, List<string> listOfGenres)
    {
        Name = name;
        ReleaseDate = releaseDate;
        AgeRating = ageRating;
        ListOfGenres = listOfGenres;
    }
    
    public bool isSuitableForAge(int age) {
        return age>= AgeRating;
    }
}

