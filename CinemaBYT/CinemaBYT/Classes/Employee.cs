public abstract class Employee : Person
{
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }

    protected Employee(DateTime hireDate, decimal salary)
    {
        HireDate = hireDate;
        Salary = salary;
    }
}