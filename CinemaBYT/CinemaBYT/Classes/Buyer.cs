using System;

public class Buyer : Person
{
    public Buyer(string name, string email, DateTime birthDate, string pesel)
        : base(name, email, birthDate, pesel)
    {
      
    }

    public Buyer()
    {
    }
    // Override Equals method
    public override bool Equals(object obj)
    {
        // If the object is null or not of the same type, return false
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        // If the object is of type Buyer, we can cast and compare properties
        Buyer otherBuyer = (Buyer)obj;

        // Delegate comparison to the base class Person.Equals method
        return base.Equals(otherBuyer); 
    }

    // Override GetHashCode method
    public override int GetHashCode()
    {
        // Delegate to base class GetHashCode
        return base.GetHashCode();
    }
}
