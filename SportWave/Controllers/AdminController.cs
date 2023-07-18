using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportWave.Services.Contracts;
using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddProductViewModel model = await adminService.GetNewAddedProductAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await adminService.AddProductAsync(model);

            if (model.Gender == "Male")
            {
                return RedirectToAction("Men", "Men");
            }
            else
            {
                return RedirectToAction("Women", "Women");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            AddCategoryViewModel model = new AddCategoryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await adminService.AddCategoryAsync(model);
            return RedirectToAction("Admin", "Admin");
        }

        public async Task<IActionResult> ManageOrders()
        {
            var model = await adminService.GetOrdersAsync();

            return View(model);
        }

        public async Task<IActionResult> SendOrder(Guid id)
        {
            await adminService.SendOrderAsync(id);

            return RedirectToAction(nameof(ManageOrders));
        }

        public async Task<IActionResult> ClearOrder(Guid id)
        {
            await adminService.ClearOrderAsync(id);

            return RedirectToAction(nameof(ManageOrders));
        }

        [HttpPost]
        public async Task<IActionResult> FilterOrders([FromForm] ManageOrdersViewModel model)
        {
            var viewModel = await adminService.GetFilteredOrdersAsync(model);
            return View(viewModel);
        }
    }
}
