using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.StoreViewModels;

namespace SportWave.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService storeService;

        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        public async Task<IActionResult> AllStores()
        {
            var model = await storeService.GetAllStoresAsync();
            if (model == null)
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public async Task<IActionResult> NearestLocation([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var allStoresModel = new AllStoresViewModel();

            try
            {
                allStoresModel.ClosestStore = await storeService.FindClosestOne(latitude, longitude);


                if (allStoresModel.Stores.Count() == 0 && allStoresModel.ClosestStore == null)
                {
                    TempData["message"] = "No Stores available!";
                    return RedirectToAction(nameof(AllStores));
                }
                else
                {
                    return View(allStoresModel);
                }
            }
            catch
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction(nameof(AllStores));
            }
        }
    }
}
