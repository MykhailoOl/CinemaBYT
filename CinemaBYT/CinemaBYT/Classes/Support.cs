
public class Support : Employee
{
    public Support(DateTime hireDate, decimal salary) : base(hireDate, salary)
    {
    }

    public string Level { get; set; }

}