
public class Manager : Employee
{
    public Manager(DateTime hireDate, decimal salary, string position, string name, string email, DateTime birthDate, string pESEL) : base(hireDate, salary, name,email,birthDate,pESEL)
    {
        Position = position;
    }

    public string Position { get; set; }

}