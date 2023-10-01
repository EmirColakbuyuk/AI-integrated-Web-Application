namespace CizgiWebServer.Model;

public class Logs
{
    public int Id { get; set; }
    
    public int user_Id { get; set; }

    public string logType { get; set; }

    public DateTime logCreatedAt { get; set; }
}