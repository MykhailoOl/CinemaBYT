using System;
using System.Diagnostics.CodeAnalysis;

public class Manager : Employee
{
    private string _position;
    [DisallowNull]    
    public string Position
    {
        get => _position;
        set => _position = string.IsNullOrWhiteSpace(value) ? throw new ArgumentNullException(nameof(Position), "Position cannot be null or empty.") : value;
    }

    public Manager(DateTime hireDate, decimal salary, string position, string name, string email, DateTime birthDate, string pesel)
        : base(hireDate, salary, name, email, birthDate, pesel)
    {
        Position = position;
    }

    public Manager(DateTime hireDate, decimal salary, string position, Person person)
        : base(hireDate, salary, person)
    {
        Position = position; 
    }

    public Manager(string position, Employee employee)
        : base(employee)
    {
        Position = position; 
    }
}
