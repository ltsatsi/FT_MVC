using FT1.Models;
using FT1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FT1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = null,
                Vehicles = null,
            };

            await userManager.CreateAsync(user, model.Password);

            await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

            ViewBag.SuccessMessage = $"An account for {user.UserName} was sucessfully created.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return NotFound();

            await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

            return RedirectToAction(actionName: "Profile", controllerName: "Home");
        }

        [HttpPost]
        [Authorize]
        public new async Task<IActionResult> SignOut()  
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
