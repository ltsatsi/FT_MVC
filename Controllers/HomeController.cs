using System.Diagnostics;
using System.Threading.Tasks;
using FT1.Models;
using FT1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FT1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return RedirectToAction(actionName: "Login", controllerName: "Account");

            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                CreatedOn = (DateTime)user.CreatedOn!
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
