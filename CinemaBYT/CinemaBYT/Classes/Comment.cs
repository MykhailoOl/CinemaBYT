using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CinemaBYT
{
    public class Comment
    {
        private string _commentText;
        private DateTime _date;
        private Movie _movie;
        private Person _person;
        private List<Comment> _replies = new List<Comment>();

        [DisallowNull]
        public string CommentText
        {
            get => _commentText;
            set => _commentText =
                value ?? throw new ArgumentNullException(nameof(CommentText), "Comment text cannot be null.");
        }

        [DisallowNull]
        public DateTime Date
        {
            get => _date;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Date cannot be in the future.");
                _date = value;
            }
        }

        [DisallowNull]
        public Movie Movie
        {
            get => _movie;
            set => _movie = value ?? throw new ArgumentNullException(nameof(Movie), "Movie cannot be null.");
        }

        [DisallowNull]
        public Person Person
        {
            get => _person;
            set => _person = value ?? throw new ArgumentNullException(nameof(Person), "Person cannot be null.");
        }

        [AllowNull]
        public List<Comment> Replies
        {
            get => _replies;
            set => _replies = value ?? new List<Comment>();
        }

        public Comment(string commentText, DateTime date, Movie movie, Person person)
        {
            CommentText = commentText;
            Date = date;
            Movie = movie;
            Person = person;
        }

        public Comment()
        {
        }

        public void AddReply(Comment reply)
        {
            if (reply == null)
                throw new ArgumentNullException(nameof(reply), "Reply cannot be null.");
            Replies.Add(reply);
        }

        //associations
        public void updateMovie(Movie movie) { 
            if (movie == null) 
                throw new ArgumentNullException(nameof(movie)); 
            if (_movie != movie) 
                _movie=movie;
        }
       
        public void deleteReply(Comment reply)
        {
            if (reply == null) throw new ArgumentNullException();
            if(_replies.Contains(reply))
                _replies.Remove(reply);
        }
        public static void deleteComment(Comment c)
        {
            //delete its mention from movie and person
            c._movie.deleteComment(c);
            if(c._person != null)
                c._person.deleteComment(c);
            //clear the comment itself
            c.Replies.ForEach(r => r = null);
            c.Replies.Clear();
            c = null;
        }

        public override string ToString()
        {
            return $"{Person.Name} commented on {Movie.Name}: \"{CommentText}\" on {Date:d}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Comment otherComment)
            {
                // Compare basic properties
                return CommentText == otherComment.CommentText &&
                       Date == otherComment.Date &&
                       Movie.Equals(otherComment.Movie) && // Assuming Movie has implemented Equals and GetHashCode
                       Person.Equals(otherComment.Person) && // Assuming Person has implemented Equals and GetHashCode
                       Replies.SequenceEqual(otherComment.Replies);
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Combine the hash codes of the relevant properties
            return HashCode.Combine(CommentText, Date, Movie, Person, Replies);
        }

        public void updateItself(Comment c)
        {
            Replies=c.Replies;
            CommentText=c.CommentText;
            Movie.updateComment(c);
        }
    }
}
