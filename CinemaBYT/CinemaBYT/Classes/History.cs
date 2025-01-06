using CinemaBYT;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class History
{
    private List<Session>? _listOfSessions;
    private Buyer _person;

    [AllowNull]
    public List<Session>? ListOfSessions
    {
        get => _listOfSessions;
        set => _listOfSessions = value;
    }

    [DisallowNull]
    public Buyer Person
    {
        get => _person;
        set => _person = value ?? throw new ArgumentNullException(nameof(Person), "Person cannot be null.");
    }

    public History(List<Session>? listOfSessions, Buyer person)
    {
        ListOfSessions = listOfSessions;
        Person = person;
    }
    public History()
    {
        _listOfSessions = new List<Session>();
    }
    public History(Buyer person)
    {
        _listOfSessions = new List<Session>();
        Person = person;
    }
    public override bool Equals(object obj)
    {
        if (obj is History otherHistory)
        {
            // Compare the person and the list of sessions, handling nulls safely.
            bool p = (Person == null && otherHistory.Person == null);
            bool l1 = ListOfSessions == null && otherHistory.ListOfSessions == null;
            bool l3=(ListOfSessions?.Capacity==0 && otherHistory.ListOfSessions?.Capacity==0) ==true;
            bool l2 = ListOfSessions?.SequenceEqual(otherHistory.ListOfSessions) == true;
            return p && (l1 || l2);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Combine the hash codes of Person and ListOfSessions, ensuring null safety.
        int personHash = Person?.GetHashCode() ?? 0; // Use 0 if Person is null
        int sessionsHash = ListOfSessions?.GetHashCode() ?? 0; // Use 0 if ListOfSessions is null
    
        return HashCode.Combine(personHash, sessionsHash);
    }
    public void deleteSessionInHistory(Session session) {
        if (session == null)
            throw new ArgumentNullException();
        if(_listOfSessions!=null)
            _listOfSessions.Remove(session);
    }
    public static void deleteHistory(History h)
    {
        if(h._person!=null)
            h._person.History = new History(new List<Session>(), h._person);
        if (h._listOfSessions != null)
        {
            h._listOfSessions.ForEach(s => s.History = null);
            h._listOfSessions.Clear();
        }
        h = null;
    }
    
    public void addSession(Session session)
    {
        if(_listOfSessions==null)
            _listOfSessions = new List<Session>();
        if(!_listOfSessions.Contains(session))
            _listOfSessions.Add(session);
    }
}
