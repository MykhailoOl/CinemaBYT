﻿using System;
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
    public override bool Equals(object obj)
    {
        if (obj is Manager otherManager)
        {
            // Compare Position and base class Employee (assumed to be implementing Equals)
            bool p = Position == otherManager.Position;
            bool b = base.Equals(otherManager);
            return p && b;
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash code of the Position and the base Employee class
        int hashCode = base.GetHashCode();
        hashCode = (hashCode * 397) ^ (Position?.GetHashCode() ?? 0); // Safe null handling for Position

        return hashCode;
    }


}
