using CinemaBYT;
using System;
using System.Collections.Generic;

public class Buyer : Person
{


    // Parameterless constructor
    public Buyer()
    {
    }

    // Copy constructor
    public Buyer(Buyer other) : base(other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other), "Buyer to copy cannot be null.");
        }
        // If Buyer has specific fields in the future, copy them here
    }

    // Constructor to inherit associations from a Person instance
    public Buyer(string name, string email, DateTime birthDate, string pesel, History history, List<Comment> comments, List<Ticket> tickets)
        : base(name, email, birthDate, pesel)
    {
        History = history; // Initialize inherited association
        comments.ForEach(addComment); // Add each comment
        tickets.ForEach(addTicket); // Add each ticket
    }
    // Primary constructor to initialize Buyer with required properties
    public Buyer(string name, string email, DateTime birthDate, string pesel)
       : base(name, email, birthDate, pesel)
    {
        History = new History(this); // Initialize inherited association
        this.Comments=new List<Comment>();
        Tickets=new List<Ticket>(); // Add each ticket
    }

    // Override Equals method
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Buyer otherBuyer = (Buyer)obj;

        // Compare properties specific to Buyer if added in the future
        // For now, delegate entirely to base class Equals
        return base.Equals(otherBuyer);
    }

    // Override GetHashCode method
    public override int GetHashCode()
    {
        // Delegate entirely to base class GetHashCode for now
        return base.GetHashCode();
    }
}
