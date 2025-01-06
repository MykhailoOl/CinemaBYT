using System;

public class DoesntOwnLoyaltyCard : Person
{
    
    public DoesntOwnLoyaltyCard(string name, string email, DateTime birthDate, string pesel)
        : base(name, email, birthDate, pesel)
    {
      
    }
    public DoesntOwnLoyaltyCard(Person p) : base(p)
    {
        Person.deletePerson(p);
    }
    public override bool Equals(object obj)
    {
        if (obj is DoesntOwnLoyaltyCard other)
        {
            // Compare properties from the base class (Person)
            return base.Equals(other); // Compare Person properties (Name, Email, BirthDate, Pesel)
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Use the base class GetHashCode to include the properties from Person
        return base.GetHashCode();
    }

}
