using Microsoft.AspNetCore.Mvc;
using CizgiWebServer.Model;
using CizgiWebServer.Data;
using CizgiWebServer.Functions;

namespace CizgiWebServer.Controllers
{
    public class TweetsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TweetsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("/Tweet")]
        public async Task<IActionResult> TweetPost([FromBody] Tweets tweet)
        {
            if (tweet == null)
            {
                return BadRequest("Invalid tweet data");
            }
    
            // Get the username from the session
            string userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
               
                return BadRequest("User not logged in");
            }

            int currentUserId = UserUtility.GetUserIdByUsername(_dbContext, userName);

            UrlGenerator urlGenerator = new UrlGenerator();
            string generatedUrl = await urlGenerator.UrlGeneratorAI();
    
            var newTweet = new Tweets
            {
                Id = tweet.Id,
                userId = currentUserId,
                contentInput = tweet.contentInput,
                CreatedAt = DateTime.Now, 
                Url = generatedUrl
            };
    
            string contentOutput = AskGpt.AIResponse(tweet.contentInput);
            newTweet.contentOutput = contentOutput;
            
            _dbContext.Tweets.Add(newTweet); 
            _dbContext.SaveChanges();
    
            UpdateLogs.AddLog(_dbContext, newTweet.userId, "Tweeted");

            return Json(new { message = "Tweet posted successfully",contentOutput = contentOutput });
        }

        [HttpGet]
        [Route("/Tweets/{userId}")]
        public IActionResult GetAllTweetsByUserId(int userId)
        {
            var tweets = _dbContext.Tweets.Where(t => t.userId == userId).ToList();
            if (tweets.Count == 0)
            {
                return NotFound("No tweets found for the user.");
            }

            UpdateLogs.AddLog(_dbContext, userId, "RequestedTweets");
            return Ok(tweets);
        }

        [HttpGet]
        [Route("/SearchTweet/{url}")]
        public IActionResult SearchTweetByURL(string url)
        {
            var tweets = _dbContext.Tweets.Where(t => t.Url.Contains(url)).ToList();
            if (tweets.Count == 0)
            {
                return NotFound("No tweets found with the specified URL.");
            }

            UpdateLogs.AddLog(_dbContext, 0, "SearchedTweets");
            return Ok(tweets);
        }
    
    }
}
