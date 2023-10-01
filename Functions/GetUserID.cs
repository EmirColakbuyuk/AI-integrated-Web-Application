using CizgiWebServer.Model;
using CizgiWebServer.Data;


namespace CizgiWebServer.Functions
{
    public static class UserUtility
    {
        public static int GetUserIdByUsername(ApplicationDbContext dbContext, string username)
        {
            Users user = dbContext.Users.FirstOrDefault(u => u.Username == username);
            return user?.userId ?? 0; // Return the userId if found, otherwise return 0
        }
    }
}