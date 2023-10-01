using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CizgiWebServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CizgiWebServer.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;

        public ProfileModel(ILogger<ProfileModel> logger)
        {
            _logger = logger;
        }

        public UserProfileModel UserProfile { get; set; }

        public IActionResult OnGet()
        {
            string userName = HttpContext.Session.GetString("UserName");
            string name = HttpContext.Session.GetString("Name");
            string surname = HttpContext.Session.GetString("Surname");
            string userMail = HttpContext.Session.GetString("UserMail");

            UserProfile = new UserProfileModel
            {
                UserName = userName,
                Name = name,
                Surname = surname,
                UserMail = userMail
            };

            
            ViewData["UserProfile"] = UserProfile;

            return Page();
        }
    }
}