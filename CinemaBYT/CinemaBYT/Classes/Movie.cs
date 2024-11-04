using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CinemaBYT.Exceptions;

namespace CinemaBYT
{
    public class Movie
    {
        private string _name;
        private DateTime _releaseDate;
        private int _ageRating;

        [Required]
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name), "Name cannot be null.");
        }

        [Required]
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new MovieException("Release date cannot be in the future.");
                }
                _releaseDate = value;
            }
        }

        [Range(0, 120, ErrorMessage = "Age rating must be between 0 and 120.")]
        public int AgeRating
        {
            get => _ageRating;
            set
            {
                if (value < 0 || value > 120)
                {
                    throw new ValidationException("Age rating must be between 0 and 120.");
                }
                _ageRating = value;
            }
        }

        [Required]
        [MinLength(1, ErrorMessage = "At least one genre must be specified.")]
        public List<string> ListOfGenres { get; set; }

        [MinLength(1)]
        public List<Session> Sessions { get; set; } = new List<Session>();

        [AllowNull]
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public Movie(string name, DateTime releaseDate, int ageRating, List<string> listOfGenres)
        {
            Name = name;
            ReleaseDate = releaseDate;
            AgeRating = ageRating;
            ListOfGenres = listOfGenres ?? throw new ArgumentNullException(nameof(listOfGenres), "List of genres cannot be null.");
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

        public void AddSession(Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session), "Session cannot be null.");
            }
            Sessions.Add(session);
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment cannot be null.");
            }
            Comments.Add(comment);
        }

        public override string ToString()
        {
            return $"{Name} (Released on {ReleaseDate:yyyy-MM-dd}), Age Rating: {AgeRating}";
        }
    }
}
