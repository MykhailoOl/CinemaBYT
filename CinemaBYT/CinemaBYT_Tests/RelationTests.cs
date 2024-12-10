namespace CinemaBYT_Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using CinemaBYT;

public class RelationTests
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
    
    [SetUp]
    public void Setup()
    {
        buyer = new Buyer("John Doe", "johndoe@example.com", new DateTime(1990, 1, 1), "12345678901");

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
        
        
        session = new Session(
            duration: new TimeSpan(2, 0, 0),
            timeStart: DateTime.Today.AddHours(14),
            income: 0.0m,
            movie: movie,
            hall: hall,
            tickets: new List<Ticket>(),
            history: history
        );
        history.ListOfSessions.Add(session);

        
        hall.Sessions.Add(session);
        
        tickets = new List<Ticket>();

        foreach (Seat s in seats)
        {
            Ticket ticket = new Ticket(s.SeatNo, 15, DateTime.Today, TicketType.Adult, session, s, buyer);  
            tickets.Add(ticket);
            s.addTicket(ticket);
        }
        session.Tickets.AddRange(tickets);

        comment = new Comment("Very good movie", DateTime.Today, movie, buyer);

        movie.Comments.Add(comment);
        buyer.co
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
    [Test]
    public void ApplyHallToOtherCinema_ShouldTransferHallSuccessfully()
    {
        Cinema newcinema = new Cinema("Lviv", "Kyiv", "Ukraine", "456456456", "10:00-20:00");
        
        hall.addCinema(newcinema);
        
        Assert.Contains(hall, newcinema.halls, "The hall should be added to the cinema.");
        Assert.AreEqual(newcinema, hall.Cinema, "The hall's cinema reference should be updated.");

    }

    [Test]
    public void UpdateMovieTest()
    {
        Movie movie1 = new Movie("movie", DateTime.Today, 16, new List<string> { "horror", "fantasy" });
        
        comment.updateMovie(movie1);    
        
        Assert.AreEqual(comment.Movie,movie1);
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
        Comment reply1 = new Comment(movie, person);
        Comment reply2 = new Comment(movie, person);
        comment.Replies.Add(reply1);
        comment.Replies.Add(reply2);

        // Verify replies are added to the comment
        Assert.AreEqual(2, comment.Replies.Count);

        // Delete the comment and ensure it handles all cases properly
        DeleteComment(comment);

        // Verify the comment is removed from movie and person
        Assert.DoesNotContain(comment, movie.GetComments());
        Assert.DoesNotContain(comment, person.GetComments());

        // Verify replies are cleared
        Assert.IsEmpty(comment.Replies);
    }
}