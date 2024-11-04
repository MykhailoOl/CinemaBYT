using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CinemaBYT
{
    public class Comment
    {
        private string _commentText;
        private DateTime _date;

        [DisallowNull]
        public string CommentText
        {
            get => _commentText;
            set => _commentText = value ?? throw new ArgumentNullException(nameof(CommentText), "Comment text cannot be null.");
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
        public Movie Movie { get; set; }

        [DisallowNull]
        public Person Person { get; set; }

        [AllowNull]
        public List<Comment> Replies { get; set; } = new List<Comment>();

        public Comment(string commentText, DateTime date, Movie movie, Person person)
        {
            CommentText = commentText;
            Date = date;
            Movie = movie;
            Person = person;
        }

        public void AddReply(Comment reply)
        {
            if (reply == null)
                throw new ArgumentNullException(nameof(reply), "Reply cannot be null.");
            Replies.Add(reply);
        }

        public override string ToString()
        {
            return $"{Person.Name} commented on {Movie.Name}: \"{CommentText}\" on {Date:d}";
        }
    }
}
