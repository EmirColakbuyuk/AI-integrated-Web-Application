using CizgiWebServer.Data;
using CizgiWebServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CizgiWebServer.Pages
{
    public class AdminPageModel : PageModel
    {
        
        private readonly ApplicationDbContext _dbContext;

        public AdminPageModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Users> Users { get; set; }
        public List<Tweets> Tweets { get; set; }
        public List<Logs> Logs { get; set; }

        public void OnGet()
        {
            Users = _dbContext.Users.ToList();
            Tweets = _dbContext.Tweets.ToList();
            Logs = _dbContext.Logs.ToList();
        }
    }

}