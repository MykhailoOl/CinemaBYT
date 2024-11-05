using System.ComponentModel.DataAnnotations;
using CinemaBYT;
using CinemaBYT.Exceptions;

namespace CinemaBYT_Tests;

public class MovieTests
    {
        private string _validName;
        private DateTime _validReleaseDate;
        private int _validAgeRating;
        private List<string> _validGenres;

        [SetUp]
        public void Setup()
        {
            _validName = "Test Movie";
            _validReleaseDate = new DateTime(2020, 1, 1);
            _validAgeRating = 18;
            _validGenres = new List<string> { "Action", "Drama" };
        }

        // Test 1: Constructor with valid parameters
        [Test]
        public void Constructor_ValidParameters_ShouldInitializeMovie()
        {
            // Act
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Assert
            Assert.AreEqual(_validName, movie.Name);
            Assert.AreEqual(_validReleaseDate, movie.ReleaseDate);
            Assert.AreEqual(_validAgeRating, movie.AgeRating);
            Assert.AreEqual(_validGenres, movie.ListOfGenres);
        }

        // Test 2: Constructor with null name
        [Test]
        public void Constructor_NullName_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Movie(null, _validReleaseDate, _validAgeRating, _validGenres));
            Assert.AreEqual("Name cannot be null. (Parameter 'Name')", exception.Message);
        }

        // Test 3: Constructor with future release date
        [Test]
        public void Constructor_FutureReleaseDate_ShouldThrowMovieException()
        {
            // Arrange
            var futureReleaseDate = DateTime.Now.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<MovieException>(() => new Movie(_validName, futureReleaseDate, _validAgeRating, _validGenres));
            Assert.AreEqual("Release date cannot be in the future.", exception.Message);
        }

        // Test 4: Constructor with invalid age rating
        [Test]
        public void Constructor_InvalidAgeRating_ShouldThrowValidationException()
        {
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() => new Movie(_validName, _validReleaseDate, -1, _validGenres));
            Assert.AreEqual("Age rating must be between 0 and 120.", exception.Message);
        }

        // Test 5: Constructor with null genres
        [Test]
        public void Constructor_NullGenres_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Movie(_validName, _validReleaseDate, _validAgeRating, null));
            Assert.AreEqual("List of genres cannot be null. (Parameter 'listOfGenres')", exception.Message);
        }

        // Test 6: IsSuitableForAge returns true for valid age
        [Test]
        public void IsSuitableForAge_ValidAge_ShouldReturnTrue()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Act
            bool result = movie.IsSuitableForAge(20);

            // Assert
            Assert.IsTrue(result);
        }

        // Test 7: IsSuitableForAge returns false for age lower than rating
        [Test]
        public void IsSuitableForAge_AgeLowerThanRating_ShouldReturnFalse()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Act
            bool result = movie.IsSuitableForAge(15);

            // Assert
            Assert.IsFalse(result);
        }

        // Test 8: IsSuitableForAge throws exception for negative age
        [Test]
        public void IsSuitableForAge_NegativeAge_ShouldThrowMovieException()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Act & Assert
            var exception = Assert.Throws<MovieException>(() => movie.IsSuitableForAge(-1));
            Assert.AreEqual("Age cannot be negative.", exception.Message);
        }

        // Test 9: IsSuitableForAge throws exception for overly high age
        [Test]
        public void IsSuitableForAge_OverlyHighAge_ShouldThrowMovieException()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Act & Assert
            var exception = Assert.Throws<MovieException>(() => movie.IsSuitableForAge(121));
            Assert.AreEqual("Age is too high to be valid.", exception.Message);
        }

        // Test 10: AddSession with a valid session
        [Test]
        public void AddSession_ValidSession_ShouldAddToSessions()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);
            var session = new Session();

            // Act
            movie.AddSession(session);

            // Assert
            Assert.Contains(session, movie.Sessions);
        }

        // Test 11: AddSession with a null session
        [Test]
        public void AddSession_NullSession_ShouldThrowArgumentNullException()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => movie.AddSession(null));
            Assert.AreEqual("Session cannot be null. (Parameter 'session')", exception.Message);
        }

        // Test 12: AddComment with a valid comment
        [Test]
        public void AddComment_ValidComment_ShouldAddToComments()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);
            var comment = new Comment();

            // Act
            movie.AddComment(comment);

            // Assert
            Assert.Contains(comment, movie.Comments);
        }

        // Test 13: AddComment with a null comment
        [Test]
        public void AddComment_NullComment_ShouldThrowArgumentNullException()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => movie.AddComment(null));
            Assert.AreEqual("Comment cannot be null. (Parameter 'comment')", exception.Message);
        }

        // Test 14: ToString method returns correct format
        [Test]
        public void ToString_ShouldReturnCorrectString()
        {
            // Arrange
            var movie = new Movie(_validName, _validReleaseDate, _validAgeRating, _validGenres);

            // Act
            string result = movie.ToString();

            // Assert
            Assert.AreEqual($"{_validName} (Released on {_validReleaseDate:yyyy-MM-dd}), Age Rating: {_validAgeRating}", result);
        }

        // Test 15: Default constructor initializes lists correctly
        [Test]
        public void DefaultConstructor_ShouldInitializeLists()
        {
            // Act
            var movie = new Movie();

            // Assert
            Assert.IsNotNull(movie.Sessions); // Sessions should be initialized
            Assert.IsNotNull(movie.Comments); // Comments should be initialized
            Assert.AreEqual(0, movie.Sessions.Count); // The list should be empty
            Assert.AreEqual(0, movie.Comments.Count); // The list should be empty
        }
    }

