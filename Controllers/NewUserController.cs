using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CizgiWebServer.Model; 
using CizgiWebServer.Data;
using CizgiWebServer.Functions;
using CizgiWebServer.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CizgiWebServer.Models;

namespace CizgiWebServer.Controllers
{
    public class NewUserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public NewUserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("/SignIn")]
        public async Task<IActionResult> CreateStudent([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("Invalid credentials!");
            }

            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return Json(new { success = false, message = "User with the certain Username already exists" });
            }

            existingUser = _dbContext.Users.FirstOrDefault(u => u.Mail == user.Mail);
            if (existingUser != null)
            {
                return Json(new
                    { success = false, message = "Mail address already exists. Please choose a different mail." });
            }

            string apiKey = "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";
            KeyGenerator keyGenerator = new KeyGenerator();
            string generatedKey = await keyGenerator.KeyGeneratorAI();

            user.Password = PasswordHasher.HashPassword(user.Password);

            int GetNextUserId()
            {
                int maxUserId = _dbContext.Users.Max(u => u.userId);
                return maxUserId + 1;
            }

            var newUser = new Users
            {
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Mail = user.Mail,
                Password = user.Password,
                Key = generatedKey
            };

            newUser.userId = GetNextUserId();
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            UpdateLogs.AddLog(_dbContext, newUser.userId, "SignIn");

            HttpContext.Session.SetInt32("UserId", newUser.userId);
            HttpContext.Session.SetString("UserName", newUser.Username);
            HttpContext.Session.SetString("UserMail", newUser.Mail);

            return Json(new { success = true, message = "User created successfully" });
        }


        [HttpPost]
        [Route("/LogInUser")]
        public IActionResult LogIn([FromBody] Users user)
        {
            var storedUser = _dbContext.Users.FirstOrDefault(u => u.Username == user.Username);

            if (storedUser == null)
            {
                return Json(new { errorMessage = "User not found" });
            }

            bool isPasswordValid = PasswordHasher.VerifyPassword(user.Password, storedUser.Password);

            if (!isPasswordValid)
            {
                return Json(new { errorMessage = "Invalid password" });
            }

            HttpContext.Session.SetInt32("UserId", storedUser.userId);
            HttpContext.Session.SetString("Name", storedUser.Name);
            HttpContext.Session.SetString("Surname", storedUser.Surname);
            HttpContext.Session.SetString("UserName", storedUser.Username);
            HttpContext.Session.SetString("UserMail", storedUser.Mail);

            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");

            UpdateLogs.AddLog(_dbContext, storedUser.userId, "LogIn");

            if (userId == 3 && userName == "BlackRose")
            {
                return Json(new { success = "admin" });
            }
            else
            {
                return Json(new { success = true });
            }

        }

        [HttpGet]
        [Route("/UserPage")]
        public IActionResult UserProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            var userMail = HttpContext.Session.GetString("UserMail");

            if (userId == null || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userMail))
            {
                return RedirectToAction("Login");
            }


            ViewBag.UserId = userId;
            ViewBag.UserName = userName;
            ViewBag.UserMail = userMail;

            if (userId == 3 && userName == "BlackRose")
            {
                return View("/Pages/AdminPage.cshtml");
            }
            else
            {
                return View("/Pages/UserPage.cshtml");
            }

        }

        [HttpPost]
        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserName", string.Empty);
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Default value is 0
            HttpContext.Session.SetInt32("UserId", 0);
            TempData["LogoutMessage"] = "You have been successfully logged out.";
            UpdateLogs.AddLog(_dbContext, userId, "Logout");

            return Json(new { success = true });
        }



        [HttpPost]
        [Route("/ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                if (!userId.HasValue)
                {
                    return BadRequest("User is not authenticated.");
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.userId == userId);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                string oldPassword = model.OldPassword;
                string newPassword = model.NewPassword;

                if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                {
                    return BadRequest("Old password and new password are required.");
                }

                bool isOldPasswordCorrect = PasswordHasher.VerifyPassword(oldPassword, user.Password );

                if (!isOldPasswordCorrect)
                {
                    return BadRequest("Old password is incorrect.");
                }

                user.Password = PasswordHasher.HashPassword(newPassword);
                _dbContext.SaveChanges();

                UpdateLogs.AddLog(_dbContext, user.userId, "ChangePassword");

                return Json(new { success = true, message = "Password changed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while changing the password.");
            }
        }
    }
}
    
    
    