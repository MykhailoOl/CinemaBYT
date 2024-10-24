
public class Manager : Employee
{
    public Manager(DateTime hireDate, decimal salary, string position) : base(hireDate, salary)
    {
        Position = position;
    }

    public string Position { get; set; }

}