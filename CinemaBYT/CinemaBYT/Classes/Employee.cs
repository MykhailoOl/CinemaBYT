using System;
using System.Diagnostics.CodeAnalysis;

public abstract class Employee : Person
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
        : base(name, email, birthDate, pesel)
    {
        HireDate = hireDate;
        Salary = salary;
    }

    protected Employee(DateTime hireDate, decimal salary, Person person)
        : base(person)
    {
        HireDate = hireDate;
        Salary = salary; 
    }

    protected Employee(Employee employee)
        : base(employee)
    {
        HireDate = employee.HireDate;
        Salary = employee.Salary; 
    }
    public override bool Equals(object obj)
    {
        if (obj is Employee otherEmployee)
        {
            // Compare HireDate, Salary, and base class Person properties
            return HireDate == otherEmployee.HireDate 
                   && Salary == otherEmployee.Salary
                   && base.Equals(otherEmployee); // Compare Person properties
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of HireDate, Salary, and base class Person properties
        return HashCode.Combine(HireDate, Salary, base.GetHashCode());
    }

}
