using CizgiWebServer.Data;
using CizgiWebServer.Model;

namespace CizgiWebServer.Functions
{
    public class UpdateLogs
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateLogs(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static void AddLog(ApplicationDbContext dbContext, int userId, string logType) // Pass dbContext as a parameter
        {
            dbContext.Logs.Add(new Logs
            {
                user_Id = userId,
                logType = logType,
                logCreatedAt = DateTime.Now
            });
            dbContext.SaveChanges();
        }
    }
}