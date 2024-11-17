using CinemaBYT;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class History
{
    private List<Session>? _listOfSessions;
    private Person _person;

    [AllowNull]
    public List<Session>? ListOfSessions
    {
        get => _listOfSessions;
        set => _listOfSessions = value;
    }

    [DisallowNull]
    public Person Person
    {
        get => _person;
        set => _person = value ?? throw new ArgumentNullException(nameof(Person), "Person cannot be null.");
    }

    public History(List<Session>? listOfSessions, Person person)
    {
        ListOfSessions = listOfSessions;
        Person = person;
    }
    public History()
    {
        _listOfSessions = new List<Session>();
    }
    public History(Person person)
    {
        _listOfSessions = new List<Session>();
        Person = person;
    }
    public override bool Equals(object obj)
    {
        if (obj is History otherHistory)
        {
            // Compare the person and the list of sessions
            return Person.Equals(otherHistory.Person) &&
                   (ListOfSessions == null && otherHistory.ListOfSessions == null || 
                    ListOfSessions?.SequenceEqual(otherHistory.ListOfSessions) == true);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of Person and ListOfSessions
        return HashCode.Combine(Person, ListOfSessions);
    }

}
