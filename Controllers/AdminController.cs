using System.Linq;
using CizgiWebServer.Data;
using CizgiWebServer.Model;
using Microsoft.AspNetCore.Mvc;

namespace CizgiWebServer.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Users = _dbContext.Users.ToList();
            ViewBag.Tweets = _dbContext.Tweets.ToList();
            ViewBag.Logs = _dbContext.Logs.ToList();

            return View();
        }

        [HttpPost]
        [Route("DeleteUser/{id}")] 
        public IActionResult DeleteUser(int id)
        {
            var userToDelete = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(userToDelete);
            _dbContext.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        [Route("/DeleteTweet/{id}")]
        public IActionResult DeleteTweet(int id)
        {
            var tweetToDelete = _dbContext.Tweets.FirstOrDefault(t => t.Id == id);

            if (tweetToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Tweets.Remove(tweetToDelete);
            _dbContext.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        [Route("/DeleteLog/{id}")]
        public IActionResult DeleteLog(int id)
        {
            var logToDelete = _dbContext.Logs.FirstOrDefault(l => l.Id == id);

            if (logToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Logs.Remove(logToDelete);
            _dbContext.SaveChanges();

            return Json(new { success = true });
        }
    }
}
