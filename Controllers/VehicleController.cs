using FT1.Models;
using FT1_ServiceLayer.ICustomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FT1.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly ICustomService<Vehicle> vehicleService;
        private readonly UserManager<ApplicationUser> userManager;
        public VehicleController(ICustomService<Vehicle> vehicleService, UserManager<ApplicationUser> userManager)
        {
            this.vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await vehicleService.GetAllAsync();
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return RedirectToAction(actionName: "Login", controllerName: "Account");

            var filterdVehicles = vehicles.Where(a => a.Id == user.Id); 

            return View(filterdVehicles);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var vehicle = await vehicleService.GetByIdAsync(id);
            return View(vehicle);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return RedirectToAction(actionName: "Login", controllerName: "Account");

            ViewData["UserId"] = user.Id;
            return View(new Vehicle());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicleModel)
        {
            if (!ModelState.IsValid)
                return View(vehicleModel);

            vehicleModel.CreatedOn = DateTime.UtcNow;
            await vehicleService.CreateAsync(vehicleModel);    

            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var vehicle = await vehicleService.GetByIdAsync(id);

            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return RedirectToAction(actionName: "Login", controllerName: "Account");

            ViewData["UserId"] = user.Id;

            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehicle vehicleModel)
        {
            if (!ModelState.IsValid)
                return View(vehicleModel);

            vehicleModel.ModifiedOn = DateTime.UtcNow;
            await vehicleService.UpdateAsync(vehicleModel);

            return RedirectToAction(actionName: "TrackFuel", controllerName: "Fuel");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var vehicle = await vehicleService.GetByIdAsync(id);
            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Vehicle vehicleModel)
        {
            await vehicleService.DeleteAsync(vehicleModel);
            return RedirectToAction(actionName: "TrackFuel", controllerName: "Fuel");
        }
    }
}
