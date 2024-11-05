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
}
