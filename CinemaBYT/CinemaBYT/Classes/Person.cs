using CinemaBYT;
using System.Diagnostics.CodeAnalysis;

public abstract class Person
{
    private string _name;
    private string _email;
    private DateTime _birthDate;
    private string _pesel;
    private List<Comment> _comments = new List<Comment>();
    private List<Ticket> _tickets = new List<Ticket>();

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

    // Expose _comments and _tickets as read-only collections
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();

    protected Person() { }

    protected Person(string name, string email, DateTime birthDate, string pesel)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        PESEL = pesel;
    }

    public void AddComment(Comment comment)
    {
        if (comment == null) throw new ArgumentNullException(nameof(comment));
        if (!_comments.Contains(comment))
        {
            _comments.Add(comment);
        }
    }

    public void RemoveComment(Comment comment)
    {
        if (comment == null) throw new ArgumentNullException(nameof(comment));
        if (!_comments.Remove(comment))
        {
            throw new InvalidOperationException("Comment not found.");
        }
    }

    public void AddTicket(Ticket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket));
        if (!_tickets.Contains(ticket))
        {
            _tickets.Add(ticket);
        }
    }

    public void RemoveTicket(Ticket ticket)
    {
        if (ticket == null) throw new ArgumentNullException(nameof(ticket));
        if (!_tickets.Remove(ticket))
        {
            throw new InvalidOperationException("Ticket not found.");
        }
    }

    public void UpdateComment(Comment updatedComment)
    {
        if (updatedComment == null) throw new ArgumentNullException(nameof(updatedComment));
        var existingComment = _comments.Find(c => c.Date == updatedComment.Date && c.Movie == updatedComment.Movie);
        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment not found for update.");
        }
        existingComment.updateItself(updatedComment);
    }

    public void UpdateItself(Person updatedPerson)
    {
        if (updatedPerson == null) throw new ArgumentNullException(nameof(updatedPerson));
        Name = updatedPerson.Name;
        Email = updatedPerson.Email;
        BirthDate = updatedPerson.BirthDate;
        PESEL = updatedPerson.PESEL;
        History = updatedPerson.History;

        // Replace internal lists with updated data
        _comments = new List<Comment>(updatedPerson._comments);
        _tickets = new List<Ticket>(updatedPerson._tickets);
    }

    public static void DeletePerson(Person person)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));

        // Delete all associated data
        foreach (var comment in person._comments.ToList())
        {
            Comment.deleteComment(comment);
        }
        person._comments.Clear();

        History.deleteHistory(person.History);

        foreach (var ticket in person._tickets.ToList())
        {
            Ticket.deleteTicket(ticket);
        }
        person._tickets.Clear();
    }

    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
            return Name == other.Name &&
                   Email == other.Email &&
                   BirthDate == other.BirthDate &&
                   PESEL == other.PESEL &&
                   (History?.Equals(other.History) ?? other.History == null) &&
                   _comments.SequenceEqual(other._comments) &&
                   _tickets.SequenceEqual(other._tickets);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Email, BirthDate, PESEL, History);
    }
}
