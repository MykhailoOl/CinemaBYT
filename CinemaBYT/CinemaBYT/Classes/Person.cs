using System;
using System.Diagnostics.CodeAnalysis;

public abstract class Person
{
    private string _name;
    private string _email;
    private DateTime _birthDate;
    private string _pesel;
    private History _history = new History();

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

    [DisallowNull]
    public History History
    {
        get => _history;
        set => _history = value ?? throw new ArgumentNullException(nameof(History), "History cannot be null.");
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
    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
            // Compare the key properties of the Person class.
            return Name == other.Name &&
                   Email == other.Email &&
                   BirthDate == other.BirthDate &&
                   PESEL == other.PESEL &&
                   EqualityComparer<History>.Default.Equals(History, other.History);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of the essential properties.
        int hashCode = Name?.GetHashCode() ?? 0;
        hashCode = (hashCode * 397) ^ (Email?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ BirthDate.GetHashCode();
        hashCode = (hashCode * 397) ^ (PESEL?.GetHashCode() ?? 0);
        hashCode = (hashCode * 397) ^ (History?.GetHashCode() ?? 0);

        return hashCode;
    }

}
