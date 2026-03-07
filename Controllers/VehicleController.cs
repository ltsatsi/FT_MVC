using FT1.Interfaces;
using FT1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FT1.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepo vehicleRepo;
        private readonly UserManager<ApplicationUser> userManager;
        public VehicleController(IVehicleRepo vehicleRepo, UserManager<ApplicationUser> userManager)
        {
            this.vehicleRepo = vehicleRepo;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var vehicles = await vehicleRepo.GetAllAsync();
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return RedirectToAction(actionName: "Login", controllerName: "Account");

            var filterdVehicles = vehicles.Where(a => a.Id == user.Id); 

            return View(filterdVehicles);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return RedirectToAction(actionName: "Login", controllerName: "Account");

            ViewData["UserId"] = user.Id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Vehicle vehicleModel)
        {
            if (!ModelState.IsValid)
                return View(vehicleModel);

            // sever-side
            vehicleModel.CreatedOn = DateTime.UtcNow;

            vehicleModel = await vehicleRepo.CreateAsync(vehicleModel);    

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
