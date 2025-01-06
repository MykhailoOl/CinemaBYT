using System;
using System.Diagnostics.CodeAnalysis;

public abstract class Employee 
{
    private DateTime _hireDate;
    private decimal _salary;

    [DisallowNull]
    public DateTime HireDate
    {
        get => _hireDate;
        set => _hireDate = value; 
    }

    [DisallowNull]
    public decimal Salary
    {
        get => _salary;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Salary), "Salary cannot be negative.");
            }
            _salary = value;
        }
    }

    protected Employee(DateTime hireDate, decimal salary, string name, string email, DateTime birthDate, string pesel)
       
    {
        HireDate = hireDate;
        Salary = salary;
    }

    protected Employee(DateTime hireDate, decimal salary, Person person)
       
    {
        HireDate = hireDate;
        Salary = salary; 
    }

    protected Employee(Employee employee)
        
    {
        HireDate = employee.HireDate;
        Salary = employee.Salary; 
    }
    public override bool Equals(object obj)
    {
        if (obj is Employee otherEmployee)
        {
            // Compare HireDate, Salary, and base class Person properties
            return HireDate == otherEmployee.HireDate &&
                   Salary == otherEmployee.Salary &&
                   base.Equals(otherEmployee); // Delegate comparison to Person class
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of HireDate, Salary, and base class Person properties
        int hashCode = HireDate.GetHashCode();
        hashCode = (hashCode * 397) ^ Salary.GetHashCode();
        hashCode = (hashCode * 397) ^ base.GetHashCode(); // Base class (Person) hash code

        return hashCode;
    }



}
