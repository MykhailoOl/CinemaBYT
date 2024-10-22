using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class Movie
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int AgeRating { get; set; }
    [MinLength(1)]
    public List<string> ListOfGenres { get; set; }

    public Movie(string name, DateTime rel, int age, List<string> genres)
    {
        
        //please check the list of genres len > 0 and release date from future
    }
}

