using CinemaBYT;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

public abstract class Person
{
    private string _name;
    private string _email;
    private DateTime _birthDate;
    private string _pesel;
    private readonly Buyer _buyer;
    private readonly Employee _employee;
    

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

  

    protected Person() { }

    protected Person(string name, string email, DateTime birthDate, string pesel, Buyer buyer)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        PESEL = pesel;
        //Buyer part
        _buyer=new Buyer(buyer);
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
            _buyer.History = other._buyer.History ?? 
            throw new ArgumentNullException(nameof(other._buyer.History), "Copied person's history cannot be null.");
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
                   (_buyer.History?.Equals(other._buyer.History) ?? other._buyer.History == null);
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
       

        return hashCode;
    }

    public void updateItself(Person p)
    {
        if (p == null) throw new ArgumentNullException();
        _name = p.Name;
        _email = p.Email;
        _birthDate = p.BirthDate;
        _pesel = p.PESEL;
    }

    public static void deletePerson(Person p)
    {
        if (p._buyer!=null)
            Buyer.deleteBuyer(p._buyer);
            p = null;
    }
}