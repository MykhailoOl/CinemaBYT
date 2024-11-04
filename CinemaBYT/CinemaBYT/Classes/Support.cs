using System;

public class Support : Employee
{
    private string _level;

    public string Level
    {
        get => _level;
        set => _level = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Level), "Level cannot be null or empty.") : value;
    }

    public Support(DateTime hireDate, decimal salary, string name, string email, DateTime birthDate, string pesel, string level)
        : base(hireDate, salary, name, email, birthDate, pesel)
    {
        Level = level; 
    }
}
