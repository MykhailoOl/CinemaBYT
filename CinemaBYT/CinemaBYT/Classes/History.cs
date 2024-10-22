using System.Diagnostics.CodeAnalysis;

public class History
{
    [AllowNull]
    public List<Session> ListOfSessions { get; set; }

    public History(List<Session> listOfSessions)
    {
        ListOfSessions = listOfSessions;
    }
    
    
}