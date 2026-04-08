using FT1.Infrastructure;
using FT1.Models;
using FT1.ViewModels;
using FT1_ServiceLayer.ICustomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FT1.Controllers
{
    [Authorize]
    public class FuelController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICustomService<Vehicle> vehicleService;
        private readonly ICustomService<FillUp> fillUpService;
        private readonly IFuelService fuelService;

        public FuelController(
            UserManager<ApplicationUser> userManager,
            ICustomService<Vehicle> vehicleService,
            ICustomService<FillUp> fillUpService,
            IFuelService fuelService)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
            this.fillUpService = fillUpService ?? throw new ArgumentNullException(nameof(fillUpService));
            this.fuelService = fuelService ?? throw new ArgumentNullException(nameof(fuelService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TrackFuel()
        {
            var user = await userManager.GetUserAsync(User);
            var vehicles = await vehicleService.GetAllAsync();
            var fillUps = await fillUpService.GetAllAsync();

            if (user is null)
                return RedirectToAction("Login", "Account");

            var filteredVehicles = vehicles.Where(v => v.Id == user.Id).ToList();
            var filteredFillUps = fillUps.Where(f => f.Vehicle!.Id == user.Id).ToList();

            var model = new TrackFuelViewModel
            {
                // statistics
                TotalVehicles = filteredVehicles.Count,
                TotalFillUps = filteredFillUps.Count,
                TotalFuelPrice = filteredFillUps.Sum(f => f.Price),
                DistanceCovered = fuelService.DistanceCovered(filteredFillUps),
                FuelConsumption = fuelService.FuelConsumption(filteredFillUps),

                // actual objects
                Vehicles = PaginatedList<Vehicle>.Create(filteredVehicles, 1, 2),
                FillUps = PaginatedList<FillUp>.Create(filteredFillUps, 1, 3),
            };

            return View(model);
        }
    }
}
