
public class Support : Employee
{
    public Support(DateTime hireDate, decimal salary, string name, string email, DateTime birthDate, string pESEL) : base(hireDate, salary, name, email,birthDate,pESEL)
    {
    }

    public string Level { get; set; }

}