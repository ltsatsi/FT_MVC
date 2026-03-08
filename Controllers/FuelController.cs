using FT1.Infrastructure;
using FT1.Interfaces;
using FT1.Models;
using FT1.Services;
using FT1.ViewModels;
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
        private readonly IVehicleRepo vehicleRepo;
        private readonly IFillUpRepo fillUpRepo;
        private readonly IFuelService fuelService;

        public FuelController(
            UserManager<ApplicationUser> userManager,
            IVehicleRepo vehicleRepo,
            IFillUpRepo fillUpRepo,
            IFuelService fuelService)
        {
            this.userManager = userManager;
            this.vehicleRepo = vehicleRepo;
            this.fillUpRepo = fillUpRepo;
            this.fuelService = fuelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TrackFuel()
        {
            var user = await userManager.GetUserAsync(User);
            var vehicles = await vehicleRepo.GetAllAsync();
            var fillUps = await fillUpRepo.GetAllAsync();

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
