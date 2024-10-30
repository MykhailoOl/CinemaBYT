public abstract class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string PESEL { get; set; }
    public History History { get; set; }

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
    protected Person(Person p2)
    {
        Name = p2.Name;
        Email = p2.Email;
        BirthDate = p2.BirthDate;
        PESEL = p2.PESEL;
    }
}