﻿using CinemaBYT;
using System;
using System.Diagnostics.CodeAnalysis;

public abstract class Person
{
    private string _name;
    private string _email;
    private DateTime _birthDate;
    private string _pesel;
    private List<Comment> _comments = new List<Comment>();
    private List<Ticket> _tickets = new List<Ticket>();
    private List<Payment> _payments = new List<Payment>();
    private Dictionary<Ticket, Payment> _ticketPaymentMap = new Dictionary<Ticket, Payment>();

    [DisallowNull]
    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Name), "Name cannot be null or empty.") : value;
    }

    [DisallowNull]
    public string Email
    {
        get => _email;
        set => _email = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Email), "Email cannot be null or empty.") : value;
    }

    [DisallowNull]
    public DateTime BirthDate
    {
        get => _birthDate;
        set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(BirthDate), "Birth date cannot be in the future.");
            }
            _birthDate = value;
        }
    }

    [DisallowNull]
    public string PESEL
    {
        get => _pesel;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 11)
            {
                throw new ArgumentException("PESEL must be a valid 11-character identifier.", nameof(PESEL));
            }
            _pesel = value;
        }
    }

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
    protected Person() { }

    protected Person(string name, string email, DateTime birthDate, string pesel)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        PESEL = pesel;
    }

    protected Person(Person other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other), "Person to copy cannot be null.");
        }

        Name = other.Name;
        Email = other.Email;
        BirthDate = other.BirthDate;
        PESEL = other.PESEL;
        History = other.History ?? throw new ArgumentNullException(nameof(other.History), "Copied person's history cannot be null.");
    }
    public void AddTicketPayment(Ticket ticket, Payment payment)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");
        if (payment == null) throw new ArgumentNullException(nameof(payment), "Payment cannot be null.");

        if (!_ticketPaymentMap.ContainsKey(ticket))
        {
            _ticketPaymentMap.Add(ticket, payment);
        }
        else
        {
            throw new ArgumentException("This ticket is already associated with a payment.");
        }
    }

    public void RemoveTicketPayment(Ticket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");

        if (!_ticketPaymentMap.Remove(ticket))
        {
            throw new KeyNotFoundException("The specified ticket is not associated with any payment.");
        }
    }

    public Payment GetPaymentForTicket(Ticket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null.");

        if (_ticketPaymentMap.TryGetValue(ticket, out Payment payment))
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

        if (_ticketPaymentMap.ContainsKey(ticket))
        {
            _ticketPaymentMap[ticket] = newPayment;
        }
        else
        {
            throw new KeyNotFoundException("The specified ticket is not associated with any payment.");
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
            // Compare all properties, considering that some might be null
            return Name == other.Name &&
                   Email == other.Email &&
                   BirthDate == other.BirthDate &&
                   PESEL == other.PESEL &&
                   (History?.Equals(other.History) ?? other.History == null);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of the essential properties, ensuring nulls are handled
        int hashCode = Name?.GetHashCode() ?? 0;
        hashCode = (hashCode * 397) ^ (Email?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ BirthDate.GetHashCode();
        hashCode = (hashCode * 397) ^ (PESEL?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ (History?.GetHashCode() ?? 0);  // Safe null handling for History

        return hashCode;
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
        if (_tickets.Count != 0)
        {
            if (!_tickets.Remove(t))
            {
                throw new ArgumentOutOfRangeException();
            }
        }
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
        if (_comments.Count != 0)
        {
            _comments.Add(c);
        }
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
    public void updateItself(Person p)
    {
        if (p == null) throw new ArgumentNullException();
        _name = p.Name;
        _email = p.Email;
        _birthDate = p.BirthDate;
        _pesel = p.PESEL;
        History = p.History;
        _comments = p._comments;
        _tickets = p._tickets;
    }

    public static void deletePerson(Person p)
    {
        p._comments.ForEach(c => Comment.deleteComment(c));
        p._comments.Clear();
        History.deleteHistory(p.History);
        p._tickets.ForEach(t => Ticket.deleteTicket(t));
        p = null;
    }
}