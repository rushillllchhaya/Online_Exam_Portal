using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Models;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogInformation("User not authenticated, redirecting to Login.");
                return RedirectToAction("Login", "Account");
            }

            _logger.LogInformation("User {User} accessed the Index page.", User.Identity.Name);
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy policy page accessed.");
            return View();
        }

        [Authorize] // Ensuring only authenticated users can access the dashboard
        public IActionResult Dashboard()
        {
            _logger.LogInformation("User {User} accessed the Dashboard.", User.Identity.Name);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred. RequestId: {RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
