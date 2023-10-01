namespace CizgiWebServer.Model;

public class Tweets
{
    public int Id { get; set; }
    
    public int userId { get; set; }
    
    public string contentInput { get; set; }

    public string contentOutput { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string Url { get; set; }

}