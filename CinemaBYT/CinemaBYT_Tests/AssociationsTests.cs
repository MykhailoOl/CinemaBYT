namespace CinemaBYT_Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using CinemaBYT;

public class AssociationsTests
{
    private Cinema cinema;
    private Hall hall;
    private List<Seat> seats;
    private Session session;
    private Movie movie;
    private History history;
    private Buyer buyer;
    private List<Ticket> tickets;
    private Comment comment;
    private Payment payment;
    
    [SetUp]
    public void Setup()
    {
        buyer = new Buyer("John Doe", "johndoe@example.com", new DateTime(1990, 1, 1),
            "12345678901",null,new List<Comment>(),new List<Ticket>());

        seats = new List<Seat>();
        for (int i = 1; i < 26; i++)
        {
            seats.Add(new Seat(i, true, null));
        }

        hall = new Hall(1, 25, seats);
        
        for (int i = 1; i < 26; i++)
        {
            seats[i - 1].addHall(hall);
        }

        cinema = new Cinema("Kyiv", "Kyiv", "Ukraine", "456456456", "10:00-20:00");

        movie = new Movie("movie", DateTime.Today, 12, new List<string> { "horror", "fantasy" });
        
        history = new History(buyer);
        buyer.History = history;
        
        session = new Session(
            duration: new TimeSpan(2, 0, 0),
            timeStart: DateTime.Today.AddHours(14),
            income: 0.0m,
            movie: movie,
            hall: hall,
            tickets: new List<Ticket>(),
            history: history
        );
        
        hall.Sessions.Add(session);
        
        tickets = new List<Ticket>();

        foreach (Seat s in seats)
        {
            Ticket ticket = new Ticket(s.SeatNo, 15, DateTime.Today, TicketType.Adult, session, s, buyer);  
            tickets.Add(ticket);
            s.addTicket(ticket);
        }
        
        comment = new Comment("Very good movie", DateTime.Today, movie, buyer);

        movie.Comments.Add(comment);
        buyer.Comments.Add(comment);
        payment = new Payment(PaymentType.Blik, DateTime.Today, 2);

    }
    //Cinema tests
    [Test]
    public void AddHall_ShouldAddHallToCinema()
    {
        // Act
        cinema.addHall(hall);

        // Assert
        Assert.Contains(hall, cinema.halls, "The hall should be added to the cinema.");
        Assert.AreEqual(cinema, hall.Cinema, "The hall's cinema reference should be updated.");
    }
        
    [Test]
    public void DeleteHall_ShouldRemoveAllAssociatedData()
    {
        cinema.addHall(hall);

        cinema.deleteHall(hall);
        
        // Check that the cinema reference in the hall is null
        Assert.IsNull(hall.Cinema, "The hall's cinema reference should be null after deleting the cinema.");

        Assert.IsFalse(cinema.halls.Contains(hall), "The cinema should not contain the hall after deletion.");

        // Check that the seats' references to the hall and ticket are null
        foreach (var seat in hall.Seats)
        {
            Assert.IsNull(seat.Hall, "Each seat's hall reference should be null after deleting the cinema.");
            Assert.IsNull(seat.Ticket, "Each seat's ticket reference should be null after deleting the cinema.");
        }

        // Check that the sessions list in the hall is cleared
        Assert.IsEmpty(hall.Sessions, "The hall's sessions list should be empty after deleting the cinema.");

        // Verify that the session's history and movie references are cleared
        Assert.IsNull(session.History, "The session's history reference should be null after deleting the cinema.");
        Assert.IsNull(session.Movie, "The session's movie reference should be null after deleting the cinema.");

        // Verify that tickets are removed and do not reference seats or sessions
        foreach (var ticket in tickets)
        {
            Assert.IsNull(ticket.Seat, "Each ticket's seat reference should be null after deleting the cinema.");
            Assert.IsNull(ticket.Session, "Each ticket's session reference should be null after deleting the cinema.");
        }


        // Verify that the history list of sessions is now empty
        /*
        Assert.IsEmpty(history.ListOfSessions, "The history's list of sessions should be empty after deleting the cinema.");
        */
        Assert.IsEmpty(movie.Sessions, "The movies list of sessions should be empty after deleting the cinema.");

    }
    
    [Test]
    public void DeleteCinema_ShouldRemoveAllAssociatedDataFromHallsAndTheirRelations()
    {
        cinema.addHall(hall);
        
        // Act
        cinema.deleteCinema();

        
        // Assert: Verify that all relationships between cinema, halls, seats, sessions, and tickets are cleared
        Assert.IsNull(hall.Cinema, "Each hall's cinema reference should be null after deleting the cinema.");
        

        // Check that the cinema's halls list is empty
        Assert.IsEmpty(cinema.halls, "The cinema's halls list should be empty after calling deleteCinema.");

        // Check that seats in all halls have their hall and ticket references cleared
        
        foreach (var seat in hall.Seats)
        {
            Assert.IsNull(seat.Hall, "Each seat's hall reference should be null after deleting the cinema.");
            Assert.IsNull(seat.Ticket, "Each seat's ticket reference should be null after deleting the cinema.");
        }

        Assert.IsEmpty(hall.Sessions, "Each hall's sessions list should be empty after deleting the cinema.");
        

        // Verify session relationships
        Assert.IsNull(session.Movie, "The session's movie reference should be null after deleting the cinema.");
        Assert.IsNull(session.History, "The session's history reference should be null after deleting the cinema.");

        // Verify ticket relationships
        foreach (var ticket in session.Tickets)
        {
            Assert.IsNull(ticket.Seat, "Each ticket's seat reference should be null after deleting the cinema.");
            Assert.IsNull(ticket.Session, "Each ticket's session reference should be null after deleting the cinema.");
        }

        /*
        Assert.IsEmpty(history.ListOfSessions, "The history's list of sessions should be empty after deleting the cinema.");
        */
        Assert.IsEmpty(movie.Sessions, "The movies list of sessions should be empty after deleting the cinema.");

    }
    [Test]
    public void ApplyHallToOtherCinema_ShouldUpdateSuccessfully()
        {
            Cinema newcinema = new Cinema("Lviv", "Kyiv", "Ukraine", "456456456", "10:00-20:00");
            
            hall.addCinema(newcinema);
            
            Assert.Contains(hall, newcinema.halls, "The hall should be added to the cinema.");
            Assert.AreEqual(newcinema, hall.Cinema, "The hall's cinema reference should be updated.");
    
        }
    [Test]
    public void DeleteHall_ShouldThrowInvalidOperationException_WhenHallDoesNotBelongToCinema()
    {
        
        var ex = Assert.Throws<InvalidOperationException>(() => cinema.deleteHall(hall));
        Assert.AreEqual("The specified hall does not belong to this cinema.", ex.Message);
    }

    [Test]
    public void DeleteTicket_ShouldThrowArgumentNullException_WhenNoTicketExists()
    {
        var seat = new Seat(27,false,hall); // A seat without a ticket assigned

        var ex = Assert.Throws<ArgumentNullException>(() => seat.deleteTicket());
        Assert.AreEqual("No seat", ex.ParamName);
    }
    
    

    
    
    
    
    //Comment tests
    [Test]
    public void UpdateMovieTest()
    {
        Movie movie1 = new Movie("movie", DateTime.Today, 16, new List<string> { "horror", "fantasy" });
        
        comment.updateMovie(movie1);    
        
        Assert.AreEqual(comment.Movie,movie1);
    }
    
    [Test]
    public void AddReply_ShouldHandleValidAndInvalidReplies()
    {
        var reply = new Comment("Very good movie", DateTime.Today, movie, buyer);

        Assert.Throws<ArgumentNullException>(
            () => comment.AddReply(null), 
            "Expected ArgumentNullException when reply is null"
        );

        comment.AddReply(reply);

        Assert.Contains(reply, comment.Replies, "Reply should be added to the Replies collection.");
    }
    [Test]
    public void DeleteReply_ShouldRemoveReply_WhenReplyExists()
    {
        // Arrange
        var reply = new Comment("Very good movie", DateTime.Today, movie, buyer);
        comment.Replies.Add(reply);

        // Act
        comment.deleteReply(reply);

        // Assert
        Assert.IsFalse(comment.Replies.Contains(reply), "Reply was not removed from the list.");
    }

    [Test]
    public void DeleteComment_HandlesAllCasesCorrectly()
    {
        // Add replies to the comment
        Comment reply1 = new Comment("Very good movie", DateTime.Today, movie, buyer);
        Comment reply2 = new Comment("Very bad movie", DateTime.Today, movie, buyer);
        comment.Replies.Add(reply1);
        comment.Replies.Add(reply2);

        // Verify replies are added to the comment
        Assert.AreEqual(2, comment.Replies.Count);

        // Delete the comment and ensure it handles all cases properly
        Comment.deleteComment(comment);

        // Verify the comment is removed from movie and person
        Assert.IsFalse(movie.Comments.Contains(comment));
        Assert.IsFalse(buyer.Comments.Contains(comment));

        // Verify replies are cleared
        Assert.IsEmpty(comment.Replies);
    }
    
    [Test]
    public void UpdateItself_UpdatesFieldsCorrectly()
    {
        
        var updatedComment = new Comment("Very nud movie", DateTime.Today, movie, buyer);
        // Act
        comment.updateItself(updatedComment);

        // Assert
        Assert.AreEqual(updatedComment.Replies, comment.Replies, "Replies were not updated correctly.");
        Assert.AreEqual(updatedComment.CommentText, comment.CommentText, "CommentText was not updated correctly.");
        Assert.Contains(updatedComment,movie.Comments);
    }
    
    
    //Hall tests
    
    [Test]
    public void AddSession_HandlesAllCasesCorrectly()
    {
        
        Assert.Throws<ArgumentNullException>(() => hall.addSession(null), "addSession did not throw ArgumentNullException for null session.");

        hall.addSession(session);
        Assert.Contains(session, hall.Sessions, "Session was not added to the hall.");
        Assert.AreEqual(hall, session.Hall, "Session's hall was not updated.");

        hall.addSession(session); // Try adding the same session again
        Assert.AreEqual(1, hall.Sessions.Count(s => s == session), "Duplicate session was added to the hall.");
    }

    //History tests
    [Test]
    public void AddSession_ShouldHandleNullListAndAddUniqueSessions()
    {
        // Act (Adding first session)
        history.addSession(session);

        // Assert (First session is added)
        Assert.NotNull(history.ListOfSessions, "The list of sessions should be initialized.");
        Assert.Contains(session, history.ListOfSessions, "Session1 should be added to the list.");

        // Act (Adding duplicate session)
        history.addSession(session);

        // Assert (Duplicate session is not added)
        Assert.AreEqual(1, history.ListOfSessions.Count, "Duplicate sessions should not be added.");

    }
    
    
    //Session Tests
    [Test]
    public void AddTicket_ShouldHandleAllScenarios()
    {
        Assert.Throws<ArgumentNullException>(() => session.AddTicket(null), "Expected ArgumentNullException for null ticket.");

        session.AddTicket(tickets[0]);

        // Assert (Ticket Added)
        Assert.Contains(tickets[0], session.Tickets, "Ticket should be added to the session's ticket list.");
        Assert.AreEqual(session, tickets[0].Session, "Ticket should reference the correct session.");

        // Act (Add Duplicate Ticket)
        session.AddTicket(tickets[0]);

        // Assert (Duplicate Ticket Not Added)
        Assert.AreEqual(1, session.Tickets.Count, "Duplicate tickets should not be added.");

    }
    [Test]
    public void DeleteTicket_ShouldHandleAllScenarios()
    {
        
        session.AddTicket(tickets[0]);

        Assert.Throws<ArgumentNullException>(() => session.DeleteTicket(null), "Expected ArgumentNullException for null ticket.");

        session.DeleteTicket(tickets[1]);

        Assert.AreEqual(1, session.Tickets.Count, "Deleting a non-existent ticket should not affect the collection.");

        session.DeleteTicket(tickets[0]);

        Assert.IsFalse(session.Tickets.Contains(tickets[0]), "Ticket should be removed from the session.");
    }
    
    [Test]
    public void MovieMethods_ShouldHandleUpdateAndReplaceScenarios()
    {
        
        Movie newMovie = new Movie("movie", DateTime.Today, 16, new List<string> { "horror", "fantasy" });

        session.UpdateMovie(movie);
        Assert.Throws<ArgumentNullException>(() => session.UpdateMovie(null), "Expected ArgumentNullException for null movie.");

        session.UpdateMovie(newMovie);
        Assert.AreEqual(newMovie, session.Movie, "Movie should be updated to the new movie.");

        Assert.Throws<ArgumentNullException>(() => session.ReplaceMovie(null), "Expected ArgumentNullException for null movie.");

        session.ReplaceMovie(newMovie);
        // Assert (No Change)
        Assert.AreEqual(newMovie, session.Movie, "Movie should not change if the same movie is provided.");

        session.ReplaceMovie(movie);

        Assert.AreEqual(movie, session.Movie, "Movie should be replaced with the new movie.");
    }
    
    [Test]
    public void UpdateHall_ShouldHandleAllScenarios()
    {
        Hall newHall = new Hall(2, 25, seats);
        session.UpdateHall(hall);
        
        Assert.Throws<ArgumentNullException>(() => session.UpdateHall(null), "Expected ArgumentNullException for null hall.");

        session.UpdateHall(hall);
        Assert.AreEqual(hall, session.Hall, "Hall should not change if the same hall is provided.");

        // Act (Update Hall with New Hall)
        session.UpdateHall(newHall);
        Assert.AreEqual(newHall, session.Hall, "Session's hall should be updated to the new hall.");
    }
    
    //Movie tests
    
    [Test]
    public void AddSession_ShouldHandleAllScenarios()
    {
        Assert.Throws<ArgumentNullException>(() => movie.AddSession(null), "Expected ArgumentNullException for null session.");

        movie.AddSession(session);
        Assert.Contains(session, movie.Sessions, "Session1 should be added to the movie's session list.");

        // Act (Add Duplicate Session)
        movie.AddSession(session);
        Assert.AreEqual(1, movie.Sessions.Count, "Duplicate sessions should not be added.");

    }
    
    [Test]
    public void AddComment_ShouldHandleAllScenarios()
    {
        
        Assert.Throws<ArgumentNullException>(() => movie.AddComment(null), "Expected ArgumentNullException for null comment.");
        movie.AddComment(comment);
        Assert.Contains(comment, movie.Comments, "Comment1 should be added to the movie's comment list.");

        // Act (Add Duplicate Comment)
        movie.AddComment(comment);
        Assert.AreEqual(1, movie.Comments.Count, "Duplicate comments should not be added.");
    }
    
    [Test]
    public void UpdateItself_ShouldUpdateFieldsFromNewMovie()
    {
        Movie newMovie = new Movie("movie", DateTime.Today, 16, new List<string> { "horror", "fantasy" });
        movie.updateItself(newMovie);

        Assert.AreEqual(16, movie.AgeRating, "AgeRating should be updated.");
        Assert.AreEqual("movie", movie.Name, "Name should be updated.");
        Assert.AreEqual(DateTime.Today, movie.ReleaseDate, "ReleaseDate should be updated.");
        CollectionAssert.AreEqual(newMovie.ListOfGenres, movie.ListOfGenres, "Genres should be updated.");
        CollectionAssert.AreEqual(newMovie.Comments, movie.Comments, "Comments should be updated.");
        CollectionAssert.AreEqual(newMovie.Sessions, movie.Sessions, "Sessions should be updated.");

        Assert.Throws<ArgumentNullException>(() => movie.updateItself(null), "Expected ArgumentNullException for null movie.");
    }
    
    [Test]
    public void DeleteMovie_ShouldDeleteSessionsAndCommentsAndNullifyMovie()
    {
        movie=Movie.deleteMovie(movie);

        //Assert.AreEqual(0, movie.Sessions.Count, "All sessions should be removed from the movie.");
        //Assert.AreEqual(0, movie.Comments.Count, "All comments should be removed from the movie.");
        
        // Assert (Movie Nullified)
        Assert.IsNull(movie, "Movie should be null after deletion.");
    }
    
    //Person tests
    
    [Test]
    public void AddTicketPayment_ShouldHandleAllScenarios_Correctly()
    {
        // Scenario 1: Adding a payment for a ticket
        buyer.AddTicketPayment(tickets[0], payment);
        Assert.AreEqual(payment, buyer.TicketPaymentMap[tickets[0].TicketId], "The payment should be associated with the ticket.");

        // Scenario 2: Trying to add a payment for a ticket that is null
        var ex1 = Assert.Throws<ArgumentNullException>(() => buyer.AddTicketPayment(null, payment));
        Assert.That(ex1.ParamName, Is.EqualTo("ticket"));
        Assert.AreEqual("Ticket cannot be null. (Parameter 'ticket')", ex1.Message);

        // Scenario 3: Trying to add a payment where the payment is null
        var ex2 = Assert.Throws<ArgumentNullException>(() => buyer.AddTicketPayment(tickets[0], null));
        Assert.That(ex2.ParamName, Is.EqualTo("payment"));
        Assert.AreEqual("Payment cannot be null. (Parameter 'payment')", ex2.Message);
    }
    
    [Test]
    public void RemoveTicketPayment_ShouldHandleAllScenarios_Correctly()
    {
        // Scenario 1: Removing a ticket payment successfully
        buyer.AddTicketPayment(tickets[0], payment);
        buyer.RemoveTicketPayment(tickets[0]);
        Assert.False(buyer.TicketPaymentMap.ContainsKey(tickets[0].TicketId), "The ticket should be removed from the payment map.");

        // Scenario 2: Trying to remove a ticket payment for a ticket that is null
        var ex1 = Assert.Throws<ArgumentNullException>(() => buyer.RemoveTicketPayment(null));
        Assert.That(ex1.ParamName, Is.EqualTo("ticket"));
        Assert.AreEqual("Ticket cannot be null. (Parameter 'ticket')", ex1.Message);

        // Scenario 3: Trying to remove a ticket payment for a ticket not in the map (ticket without payment)
        var ex2 = Assert.Throws<KeyNotFoundException>(() => buyer.RemoveTicketPayment(tickets[0]));
        Assert.AreEqual("The specified ticket is not associated with any payment.", ex2.Message);
    }
    
    [Test]
    public void GetPaymentForTicket_ShouldHandleAllScenarios_Correctly()
    {
        // Scenario 1: Retrieving payment for an existing ticket
        buyer.AddTicketPayment(tickets[0], payment); // First, add a payment
        var retrievedPayment = buyer.GetPaymentForTicket(tickets[0]);
        Assert.AreEqual(payment, retrievedPayment, "The retrieved payment should match the expected payment.");

        // Scenario 2: Trying to retrieve a payment for a null ticket
        var ex1 = Assert.Throws<ArgumentNullException>(() => buyer.GetPaymentForTicket(null));
        Assert.That(ex1.ParamName, Is.EqualTo("ticket"));
        Assert.AreEqual("Ticket cannot be null. (Parameter 'ticket')", ex1.Message);

        // Scenario 3: Trying to retrieve a payment for a ticket that doesn't exist in the map
        var ex2 = Assert.Throws<KeyNotFoundException>(() => buyer.GetPaymentForTicket(tickets[1]));
        Assert.AreEqual("The specified ticket is not associated with any payment.", ex2.Message);
    }
    
    [Test]
    public void UpdateTicketPayment_ShouldHandleAllScenarios_Correctly()
    {
        // Scenario 1: Successfully updating a ticket's payment
        Payment _newPayment = new Payment(PaymentType.Blik, DateTime.Today, 1);
        buyer.AddTicketPayment(tickets[0], payment);
        buyer.UpdateTicketPayment(tickets[0],_newPayment);
        
        Assert.AreEqual(_newPayment, buyer.TicketPaymentMap[tickets[0].TicketId], "The payment should be updated to the new payment.");

        // Scenario 2: Trying to update a payment for a null ticket
        var ex1 = Assert.Throws<ArgumentNullException>(() => buyer.UpdateTicketPayment(null, _newPayment));
        Assert.That(ex1.ParamName, Is.EqualTo("ticket"));
        Assert.AreEqual("Ticket cannot be null. (Parameter 'ticket')", ex1.Message);

        // Scenario 3: Trying to update a payment for a null payment
        var ex2 = Assert.Throws<ArgumentNullException>(() => buyer.UpdateTicketPayment(tickets[0], null));
        Assert.That(ex2.ParamName, Is.EqualTo("newPayment"));
        Assert.AreEqual("Payment cannot be null. (Parameter 'newPayment')", ex2.Message);

        // Scenario 4: Trying to update a payment for a ticket not associated with any payment
        var ex3 = Assert.Throws<KeyNotFoundException>(() => buyer.UpdateTicketPayment(tickets[1], _newPayment));
        Assert.AreEqual("The specified ticket is not associated with any payment.", ex3.Message);
    }
    
    
    
        [Test]
        public void BuyTicket_ShouldHandleAllScenariosCorrectly()
        {
            // Act & Assert (Null Checks)
            Assert.Throws<ArgumentNullException>(() => buyer.BuyTicket(null, payment), "Expected ArgumentNullException when ticket is null");
            Assert.Throws<ArgumentNullException>(() => buyer.BuyTicket(tickets[0], null), "Expected ArgumentNullException when payment is null");

            buyer.BuyTicket(tickets[0], payment);

            Assert.Contains(tickets[0], buyer.Tickets, "Ticket should be added to person's ticket collection");
            Assert.Contains(payment, buyer.Payments, "Payment should be added to person's payment collection");

            Assert.AreEqual(buyer, payment.Person, "Payment should reference the correct person");
            Assert.AreEqual(tickets[0], payment.Ticket, "Payment should reference the correct ticket");
            Assert.AreEqual(buyer, tickets[0].Person, "Ticket should reference the correct person");
            Assert.Contains(payment, tickets[0].Payments, "Ticket should contain the payment in its payments collection");
        }
        
        
        
        
        [Test]
        public void AddAndDeleteTicket_ShouldAddAndDeleteTicketSuccessfully()
        {
            // Add ticket
            buyer.addTicket(tickets[0]);
            Assert.Contains(tickets[0], buyer.Tickets);  
            // Try adding the same ticket again (should not throw exception, just do nothing)
            Assert.DoesNotThrow(() => buyer.addTicket(tickets[0])); 

            // Delete ticket
            buyer.deleteTicket(tickets[0]);
            Assert.IsFalse(buyer.Tickets.Contains(tickets[0])); 

            // Try deleting a ticket that doesn't exist (should throw ArgumentOutOfRangeException)
            Assert.Throws<ArgumentOutOfRangeException>(() => buyer.deleteTicket(tickets[0]));

            // Try deleting a null ticket (should throw ArgumentNullException)
            Assert.Throws<ArgumentNullException>(() => buyer.deleteTicket(null));
        }

        [Test]
        public void AddAndUpdateComment_ShouldAddAndUpdateCommentSuccessfully()
        {
            // Test addComment with valid comment (should add comment)
            buyer.addComment(comment);
            Assert.Contains(comment, buyer.Comments); 

            // Test addComment with null comment (should throw ArgumentNullException)
            Assert.Throws<ArgumentNullException>(() => buyer.addComment(null)); 

            // Test updateComment with valid comment (should update the existing comment)
            Comment comment2 = comment;
            comment2.CommentText = "mid";
            buyer.updateComment(comment2); 
            Assert.AreEqual("mid", comment.CommentText);
        }

        
        [Test]
        public void UpdateItself_ShouldUpdatePersonFieldsCorrectly()
        {
            Buyer newBuyer = new Buyer("newBuyer", "kwejfkjwn@gmail.com", DateTime.Today, "05232213491");

            buyer.updateItself(newBuyer);

            Assert.AreEqual("newBuyer", buyer.Name);
            Assert.AreEqual("kwejfkjwn@gmail.com", buyer.Email);
            Assert.AreEqual(DateTime.Today, buyer.BirthDate.Date); 
            Assert.AreEqual("05232213491", buyer.PESEL);
            Assert.AreEqual(new History(newBuyer), buyer.History);  
            Assert.AreEqual(new List<Comment>(), buyer.Comments);  
            Assert.AreEqual(new List<Ticket>(), buyer.Tickets);   
        }

        [Test]
public void DeletePerson_ShouldDeleteCommentsHistoryAndTickets()
{
    
    buyer.Comments.Add(comment);
    buyer.Tickets.AddRange(tickets);
    
    // Act: Call the deletePerson method to delete the person
    Person.deletePerson(buyer);

    // Assert: Verify that the person's comments list is empty
    Assert.IsEmpty(buyer.Comments, "Person's comments should be deleted.");

    // Assert: Verify that the person's tickets list is empty
    Assert.IsEmpty(buyer.Tickets, "Person's tickets should be deleted.");

    // Assert: Verify that the person's history is deleted (assuming that deleteHistory performs the deletion correctly)
    Assert.IsNull(buyer.History, "Person's history should be deleted.");

    // Verify that the comments' references have been cleared from the Comment list
    Assert.IsFalse(comment.Person.Comments.Contains(comment), "Comment should be removed from its person's comments list.");
    
    // Verify that the tickets' references have been cleared from the Ticket list
    foreach (var ticket in tickets)
    {
        Assert.IsFalse(ticket.Person.Tickets.Contains(ticket), "Ticket should be removed from its person's tickets list.");
    }
    
}
}