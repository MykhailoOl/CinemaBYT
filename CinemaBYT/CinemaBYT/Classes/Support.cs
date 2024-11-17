using System;
using System.Diagnostics.CodeAnalysis;

public class Support : Employee
{
    private string _level;

    [DisallowNull]
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
    public override bool Equals(object obj)
    {
        if (obj is Support other)
        {
            // Compare the properties that uniquely identify a Support instance.
            return base.Equals(obj) && // Delegate to the Employee class Equals method
                   (Level == other.Level); // Compare Level, handling nulls
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of the relevant properties, including the base Employee class's hash code.
        int hashCode = base.GetHashCode();
        hashCode = (hashCode * 397) ^ (Level?.GetHashCode() ?? 0); // Use null-coalescing operator to handle null values

        return hashCode;
    }


}
