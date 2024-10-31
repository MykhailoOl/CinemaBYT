
public class Support : Employee
{
    public Support(DateTime hireDate, decimal salary, string name, string email, DateTime birthDate, string pESEL, string level) : base(hireDate, salary, name, email,birthDate,pESEL)
    {
        Level = level;
    }

    public string Level { get; set; }

}