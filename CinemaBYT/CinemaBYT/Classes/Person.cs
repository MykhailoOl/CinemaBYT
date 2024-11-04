using System;

public abstract class Person
{
    private string _name;
    private string _email;
    private DateTime _birthDate;
    private string _pesel;

    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Name), "Name cannot be null or empty.") : value;
    }

    public string Email
    {
        get => _email;
        set => _email = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Email), "Email cannot be null or empty.") : value;
    }

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
        History = other.History;
    }
}
