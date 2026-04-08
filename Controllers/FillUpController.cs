using FT1.Models;
using FT1_ServiceLayer.ICustomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FT1.Controllers
{
    [Authorize]
    public class FillUpController : Controller
    {   
        private readonly ICustomService<FillUp> fillUpService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICustomService<Vehicle> vehicleService;
        public FillUpController(ICustomService<FillUp> fillUpService, UserManager<ApplicationUser> userManager, ICustomService<Vehicle> vehicleService)
        {
            this.fillUpService = fillUpService ?? throw new ArgumentNullException(nameof(fillUpService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
        } // end constructor

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var fillUps = await fillUpService.GetAllAsync();
            var filteredFillUps = fillUps.Where(f => f.Vehicle!.Id == user.Id).ToList();

            return View(filteredFillUps);
        } // end method

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var fillUp = await fillUpService.GetByIdAsync(id);
            return View(fillUp);
        } // end method

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var vehicles = await vehicleService.GetAllAsync(); 
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
        } // end method

        [HttpPost]
        public async Task<IActionResult> Create(FillUp fillUpModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FailedToCreate = "Could not create fill up";
                return View(fillUpModel);
            }

            fillUpModel.CreatedOn = DateTime.UtcNow;
            await fillUpService.CreateAsync(fillUpModel);

            return View(fillUpModel);
        } // end method

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fillUp = await fillUpService.GetByIdAsync(id);
            ViewBag.VehicleId = fillUp.VehicleId;
            return View(fillUp);
        } // end method

        [HttpPost]
        public async Task<IActionResult> Edit(FillUp fillUp)
        {
            if(!ModelState.IsValid)
                return View(fillUp);

            await fillUpService.UpdateAsync(fillUp);
            return RedirectToAction(controllerName: "Fuel", actionName: "TrackFuel");
        } // end method

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fillUp = await fillUpService.GetByIdAsync(id);
            return View(fillUp);
        } // end method

        [HttpPost]
        public async Task<IActionResult> Delete(FillUp fillUp)
        {
            await fillUpService.DeleteAsync(fillUp);
            return View(fillUp);
        } // end method
    } // end class
} // end namespace
