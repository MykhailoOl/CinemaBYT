public class Comment
{
    public string CommentText { get; set; }
    public DateTime Date { get; set; }
    public Movie Movie { get; set; }

    public Comment(string commentText, DateTime date, Movie movie)
    {
        CommentText = commentText;
        Date = date;
        Movie = movie;
    }
}