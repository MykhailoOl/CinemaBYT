using System.Diagnostics.CodeAnalysis;
using System.Transactions;

public abstract class Employee : Person
{
    [DisallowNull]
    public DateTime HireDate { get; set; }
    [DisallowNull]
    public decimal Salary { get; set; }

    protected Employee(DateTime hireDate, decimal salary, string name, string email, DateTime birthDate, string pESEL) : base(name, email, birthDate, pESEL)
    {
        HireDate = hireDate;
        Salary = salary;
    }
    protected Employee(DateTime hireDate, decimal salary, Person p) : base(p)
    {
        HireDate = hireDate;
        Salary = salary;
    }
    protected Employee(Employee employee)
    {
        HireDate = employee.HireDate;
        Salary = employee.Salary;
    }

}