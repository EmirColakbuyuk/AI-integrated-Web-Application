using Microsoft.AspNetCore.Mvc;
using CizgiWebServer.Data;
using CizgiWebServer.Model;

namespace CizgiWebServer.Controllers
{
    public class LogController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LogController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/Log/{userId}")]
        public IActionResult GetLogsByUserId(int userId)
        {
            var logs = _dbContext.Logs.Where(log => log.user_Id == userId).ToList();
            if (logs.Count == 0)
            {
                return NotFound("No logs found for the user.");
            }

            return Ok(logs);
        }
    }
}