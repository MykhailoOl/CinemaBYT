using CinemaBYT;
using System;
using System.Diagnostics.CodeAnalysis;

public abstract class Person
{
    private string _name;
    private string _email;
    private DateTime _birthDate;
    private string _pesel;
    private List<Comment> _comments = new List<Comment>();
    private List<Ticket> tickets = new List<Ticket>();

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
        if (tickets.Count != 0)
        {
            if(!tickets.Remove(t))
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
    public void addTicket(Ticket t)
    {
        if(t == null) throw new ArgumentNullException();
        if(!tickets.Contains(t))
        {
            tickets.Add(t);
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
                if(oldC != null)
                     oldC.updateItself(c);
            }
        }
    }
    public void updateItself(Person p)
    {
        if (p == null) throw new ArgumentNullException();
        _name=p.Name;
        _email=p.Email;
        _birthDate=p.BirthDate;
        _pesel = p.PESEL;
        History= p.History;
        _comments=p._comments;
        tickets = p.tickets;
    }

    public static void deletePerson(Person p)
    {
        p._comments.ForEach(c=> Comment.deleteComment(c));
        p._comments.Clear();
        History.deleteHistory(p.History);
        p.tickets.ForEach(t => Ticket.deleteTicket(t));
        p = null;
    }
}
