
public class Manager : Employee
{
    public string Position { get; set; }
    public Manager(DateTime hireDate, decimal salary, string position, string name, string email, DateTime birthDate, string pESEL) : base(hireDate, salary, name,email,birthDate,pESEL)
    {
        Position = position;
    }
    public Manager (DateTime hireDate, decimal salary, string position, Person p) : base(hireDate, salary, p)
    {
        Position = position;
    }
    public Manager(string position, Employee e) : base(e)
    {
        Position = position;
    }



}