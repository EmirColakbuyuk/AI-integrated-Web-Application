// UserProfileModel.cs
namespace CizgiWebServer.Models
{
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserMail { get; set; }
    }
}