using CinemaBYT;

namespace CinemaBYT_Tests;

public class CommentTests
    {
        private Movie _movie;
        private Buyer _person;
        private Comment _comment;

        [SetUp]
        public void Setup()
        {
            
            _movie = new Movie();
            _person = new Buyer(); 
            _comment = new Comment("Great movie!", DateTime.Now, _movie, _person);
        }

        // Constructor tests
        [Test]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var date = DateTime.Now;
            
            // Act
            var comment = new Comment("Great movie!", date, _movie, _person);
            
            // Assert
            Assert.AreEqual("Great movie!", comment.CommentText);
            Assert.AreEqual(date, comment.Date);
            Assert.AreEqual(_movie, comment.Movie);
            Assert.AreEqual(_person, comment.Person);
            Assert.IsNotNull(comment.Replies);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCommentTextIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Comment(null, DateTime.Now, _movie, _person));
            Assert.That(exception.Message, Does.Contain("Comment text cannot be null").IgnoreCase);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMovieIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Comment("Great movie!", DateTime.Now, null, _person));
            Assert.That(exception.Message, Does.Contain("Movie cannot be null").IgnoreCase);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenPersonIsNull()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Comment("Great movie!", DateTime.Now, _movie, null));
            Assert.That(exception.Message, Does.Contain("Person cannot be null").IgnoreCase);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenDateIsInTheFuture()
        {
            // Act & Assert
            var futureDate = DateTime.Now.AddDays(1);
            var exception = Assert.Throws<ArgumentException>(() => new Comment("Great movie!", futureDate, _movie, _person));
            Assert.That(exception.Message, Does.Contain("Date cannot be in the future").IgnoreCase);
        }

        // Property tests

        [Test]
        public void CommentText_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _comment.CommentText = null);
            Assert.That(exception.Message, Does.Contain("Comment text cannot be null").IgnoreCase);
        }

        [Test]
        public void Date_SetToFutureDate_ShouldThrowArgumentException()
        {
            // Act & Assert
            var futureDate = DateTime.Now.AddDays(1);
            var exception = Assert.Throws<ArgumentException>(() => _comment.Date = futureDate);
            Assert.That(exception.Message, Does.Contain("Date cannot be in the future").IgnoreCase);
        }

        [Test]
        public void Movie_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _comment.Movie = null);
            Assert.That(exception.Message, Does.Contain("Movie cannot be null").IgnoreCase);
        }

        [Test]
        public void Person_SetToNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _comment.Person = null);
            Assert.That(exception.Message, Does.Contain("Person cannot be null").IgnoreCase);
        }

        // Valid value tests

        [Test]
        public void CommentText_ShouldAcceptValidValue()
        {
            // Act
            _comment.CommentText = "A new comment text";

            // Assert
            Assert.AreEqual("A new comment text", _comment.CommentText);
        }

        [Test]
        public void Date_ShouldAcceptValidDate()
        {
            // Arrange
            var validDate = DateTime.Now.AddDays(-1);

            // Act
            _comment.Date = validDate;

            // Assert
            Assert.AreEqual(validDate, _comment.Date);
        }

        [Test]
        public void Replies_ShouldBeEmptyListByDefault()
        {
            // Assert
            Assert.IsNotNull(_comment.Replies);
            Assert.IsEmpty(_comment.Replies);
        }
    }