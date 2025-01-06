using CinemaBYT;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class Buyer 
{
    private List<Comment> _comments = new List<Comment>();
    private List<Ticket> _tickets = new List<Ticket>();
    private List<Payment> _payments = new List<Payment>();
    private Dictionary<int, Payment> _ticketPaymentMap = new Dictionary<int, Payment>();

    [AllowNull]
    public History History { get; set; } = new History();
    [AllowNull]
    public List<Payment> Payments
    {
        get => _payments;
        set => _payments = value ?? new List<Payment>();
    }


    [AllowNull]
    public List<Ticket> Tickets
    {
        get => _tickets;
        set => _tickets = value;
    }
    [AllowNull]
    public List<Comment> Comments
    {
        get => _comments;
        set => _comments = value;
    }

    public Dictionary<int, Payment> TicketPaymentMap
    {
        get => _ticketPaymentMap;
        set => _ticketPaymentMap = value ?? throw new ArgumentNullException(nameof(value));
    }


    // Parameterless constructor
    public Buyer()
    {
        this.Comments = new List<Comment>();
        Tickets = new List<Ticket>(); // Add each ticket
    }

    // Copy constructor
    public Buyer(Buyer other) 
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other), "Buyer to copy cannot be null.");
        }
        // If Buyer has specific fields in the future, copy them here
    }

    // Constructor to inherit associations from a Person instance
    public Buyer(History history, List<Comment> comments, List<Ticket> tickets)
    {
        History = history; // Initialize inherited association
        comments.ForEach(addComment); // Add each comment
        tickets.ForEach(addTicket); // Add each ticket
    }
    // Primary constructor to initialize Buyer with required properties
    //public Buyer()
    //{
    //    History = new History(this); // Initialize inherited association
    //    this.Comments=new List<Comment>();
    //    Tickets=new List<Ticket>(); // Add each ticket
    //}
    public void AddTicketPayment(Ticket ticket, Payment payment)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");
        if (payment == null) throw new ArgumentNullException(nameof(payment), "Payment cannot be null.");

        if (!_ticketPaymentMap.ContainsKey(ticket.TicketId))
        {
            //_ticketPaymentMap.Keys.Append(ticket);
            //Payment exp=_ticketPaymentMap.GetValueOrDefault(ticket);
            //_ticketPaymentMap.Append(new KeyValuePair<Ticket, Payment>(ticket, payment));
            //_ticketPaymentMap[ticket] = payment;
            //_ticketPaymentMap.TryAdd(ticket, payment);           
            //_ticketPaymentMap[ticket] = payment;
            TicketPaymentMap.Add(ticket.TicketId, payment);
        }
        else
        {
            throw new ArgumentException("This ticket is already associated with a payment.");
        }
    }

    public void RemoveTicketPayment(Ticket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");

        if (!_ticketPaymentMap.Remove(ticket.TicketId))
        {
            throw new KeyNotFoundException("The specified ticket is not associated with any payment.");
        }
    }

    public Payment GetPaymentForTicket(Ticket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");

        if (_ticketPaymentMap.TryGetValue(ticket.TicketId, out Payment payment))
        {
            return payment;
        }
        else
        {
            throw new KeyNotFoundException("The specified ticket is not associated with any payment.");
        }
    }

    public void UpdateTicketPayment(Ticket ticket, Payment newPayment)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");
        if (newPayment == null) throw new ArgumentNullException(nameof(newPayment), "Payment cannot be null.");

        if (_ticketPaymentMap.ContainsKey(ticket.TicketId))
        {
            _ticketPaymentMap[ticket.TicketId] = newPayment;
        }
        else
        {
            throw new KeyNotFoundException("The specified ticket is not associated with any payment.");
        }
    }
    public void BuyTicket(Ticket ticket, Payment payment)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket));
        if (payment == null) throw new ArgumentNullException(nameof(payment));
        AddTicketPayment(ticket, payment);
        _tickets.Add(ticket);
        _payments.Add(payment);
        //payment.setTicket(ticket);
        //payment.setPerson(this);
        //ticket.addPayment(payment);
        //ticket.setPerson(this);
    }

    public void deleteComment(Comment c)
    {
        if (c == null)
            throw new ArgumentNullException();
        if (_comments.Count != 0)
        {
            if (!_comments.Remove(c))
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        else
            throw new Exception();
    }
    public void deleteTicket(Ticket t)
    {
        if (t == null)
            throw new ArgumentNullException();

        if (!_tickets.Contains(t)) // Check if the ticket exists in the list
        {
            throw new ArgumentOutOfRangeException();
        }

        _tickets.Remove(t); // Remove the ticket if it exists
    }
    public void addTicket(Ticket t)
    {
        if (t == null) throw new ArgumentNullException();
        if (!_tickets.Contains(t))
        {
            _tickets.Add(t);
        }
    }
    public void addComment(Comment c)
    {
        if (c == null)
            throw new ArgumentNullException();

        _comments.Add(c);

    }

    public void updateComment(Comment c)
    {
        if (c == null) throw new ArgumentNullException();
        if (c.Person.Equals(this))
        {
            if (_comments.Count != 0)
            {
                Comment oldC = _comments.Find(sc => sc.Date == c.Date && sc.Movie == c.Movie);
                if (oldC != null)
                    oldC.updateItself(c);
            }
        }
    }
    public static void deleteBuyer(Buyer p)
    {
        while (p._comments.Count > 0)
        {
            try
            {
                Comment.deleteComment(p._comments[p._comments.Count - 1]);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        p._comments.Clear();
        History.deleteHistory(p.History);
        p.History = null;
        //p._tickets.ForEach(t => Ticket.deleteTicket(t));
        while (p._tickets.Count > 0)
        {
            Ticket.deleteTicket(p._tickets[p._tickets.Count - 1]);
        }
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
