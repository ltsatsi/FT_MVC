using FT1.Interfaces;
using FT1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FT1.Controllers
{
    public class FillUpController : Controller
    {
        private readonly IFillUpRepo fillUpRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVehicleRepo vehicleRepo;
        public FillUpController(IFillUpRepo fillUpRepo, UserManager<ApplicationUser> userManager, IVehicleRepo vehicleRepo)
        {
            this.fillUpRepo = fillUpRepo;
            this.userManager = userManager;
            this.vehicleRepo = vehicleRepo;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var fillUps = await fillUpRepo.GetAllAsync();
            var filteredFillUps = fillUps.Where(f => f.Vehicle!.Id == user.Id).ToList();

            return View(filteredFillUps);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var vehicles = await vehicleRepo.GetAllAsync(); 
            var filteredVehicles = vehicles.Where(v => v.Id == user.Id).ToList();

            ViewBag.VehicleId = new SelectList(
                filteredVehicles.Select(v => new
                {
                    v.VehicleId,
                    Name = v.Registration + " - " + v.Make + " " + v.Model
                }),
                "VehicleId",
                "Name"
            );

            return View(new FillUp());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FillUp fillUpModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FailedToCreate = "Could not create fill up";
                return View(fillUpModel);
            }

            fillUpModel.CreatedOn = DateTime.UtcNow;
            await fillUpRepo.CreateAsync(fillUpModel);

            return View(fillUpModel);
        }
    }
}
