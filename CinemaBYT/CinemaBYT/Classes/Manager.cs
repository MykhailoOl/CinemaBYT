
public class Manager : Employee
{
    public Manager(DateTime hireDate, decimal salary) : base(hireDate, salary)
    {
    }

    public string Position { get; set; }

}