using System.Diagnostics.CodeAnalysis;

public class Comment
{
    [DisallowNull]
    public string CommentText { get; set; }
    [DisallowNull]
    public DateTime Date { get; set; }
    [DisallowNull]
    public Movie Movie { get; set; }
    [DisallowNull]
    public Person Person { get; set; }
    [AllowNull]
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public Comment(string commentText, DateTime date, Movie movie)
    {
        CommentText = commentText;
        Date = date;
        Movie = movie;
    }
}