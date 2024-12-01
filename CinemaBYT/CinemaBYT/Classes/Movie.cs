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
        private DateTime? _releaseDate;
        private int _ageRating;
        private List<string> _listOfGenres;
        private List<Session> _sessions = new List<Session>();
        private List<Comment> _comments = new List<Comment>();

        [DisallowNull]
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name), "Name cannot be null.");
        }

        [AllowNull]
        public DateTime? ReleaseDate
        {
            get => _releaseDate;
            set
            {
                if (value.HasValue && value.Value > DateTime.Now)
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
        public List<string> ListOfGenres
        {
            get => _listOfGenres;
            set => _listOfGenres = value ?? throw new ArgumentNullException(nameof(ListOfGenres), "List of genres cannot be null.");
        }

        [MinLength(1)]
        public List<Session> Sessions 
        {
            get => _sessions;
            set => _sessions = value ?? new List<Session>();
        }

        //public void addSession(Session session) {
        //    if (!_sessions.Contains(session)){
        //        _sessions.Add(session);
        //        session.addMovie(this); 
        //    }
        //}
        //public void deleteSession(Session sesion) {
        //    if (!this.Sessions.Contains(sesion))
        //    {
        //        throw new InvalidOperationException("The specified session does not belong to this movie.");
        //    }
        //    this.Sessions.Remove(sesion);
        //    sesion.Movie
        //}
        //public void updateSession() { 
        
        //}
        [AllowNull]
        public List<Comment> Comments
        {
            get => _comments;
            set => _comments = value ?? new List<Comment>();
        }

        public Movie(string name, DateTime? releaseDate, int ageRating, List<string> listOfGenres)
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
            string releaseDateString = ReleaseDate.HasValue ? ReleaseDate.Value.ToString("yyyy-MM-dd") : "N/A";
            return $"{Name} (Released on {releaseDateString}), Age Rating: {AgeRating}";
        }
    
        public Movie()
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is Movie other)
            {
                return Name == other.Name &&
                       ReleaseDate == other.ReleaseDate &&
                       AgeRating == other.AgeRating;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = Name?.GetHashCode() ?? 0;
            hashCode = (hashCode * 397) ^ (ReleaseDate?.GetHashCode() ?? 0);
            hashCode = (hashCode * 397) ^ AgeRating.GetHashCode();
            return hashCode;
        }


    }
}
