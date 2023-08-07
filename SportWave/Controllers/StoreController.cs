using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportWave.IPInfo;
using SportWave.Services.Contracts;
using SportWave.ViewModels.StoreViewModels;
using System.Globalization;
using System.Net;

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

        public IActionResult RequestLocation()
        {
            return View();
        }

        public async Task<IActionResult> NearestLocation()
        {
            var ipInfo = new IpInfo();
            var storeModel = new StoreViewModel();
            var allStoresModel = new AllStoresViewModel();

            try
            {
                string url = "https://ipinfo.io?token=27df0381695f75";
                var info = new WebClient().DownloadString(url);

                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                if (ipInfo != null)
                {
                    RegionInfo myRI = new RegionInfo(ipInfo.Country);

                    ipInfo.Country = myRI.EnglishName;
                    storeModel.City = ipInfo.City;
                    storeModel.Region = ipInfo.Region;
                    storeModel.Location = ipInfo.Location;


                    if (storeModel.City != null)
                    {
                        allStoresModel = await storeService.GetStoreByCityOrRegionAsync(storeModel.City, storeModel.Region);

                        var yourLon = double.Parse(storeModel.Location.Split(",").ToArray()[0], CultureInfo.InvariantCulture);
                        var yourlat = double.Parse(storeModel.Location.Split(",").ToArray()[1], CultureInfo.InvariantCulture);

                        allStoresModel.ClosestStore = await storeService.FindClosestOne(yourlat, yourLon);
                    }
                }

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
