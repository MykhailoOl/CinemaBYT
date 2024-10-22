public abstract class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string PESEL { get; set; }

    protected Person()
    {
    }

    protected Person(string name, string email, DateTime birthDate, string pESEL)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        PESEL = pESEL;
    }
}