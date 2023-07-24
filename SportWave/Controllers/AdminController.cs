using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> MakeUserAdmin()
        {
            MakeUserAdminViewModel model = await adminService.GetAdminEmailsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MakeUserAdmin(MakeUserAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await adminService.MakeUserAdminAsync(model);

            return RedirectToAction(nameof(MakeUserAdmin));
        }

        [HttpGet]
        public IActionResult AddCategory()
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

            if(model == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
            if(viewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> ManagePromoCodes()
        {
            var model = await adminService.GetPromoCodesAsync();

            if(model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> MakeInvalid([FromRoute]Guid id)
        {
            var promoCode = await adminService.GetPromoCodeToChangeStatusAsync(id);

            if(promoCode == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            await adminService.MakeInvalidAsync(promoCode);
            
            return RedirectToAction(nameof(ManagePromoCodes));
        }

        public async Task<IActionResult> MakeValid([FromRoute]Guid id)
        {
            var promoCode = await adminService.GetPromoCodeToChangeStatusAsync(id);

            if(promoCode == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await adminService.MakeValidAsync(promoCode);

            return RedirectToAction(nameof(ManagePromoCodes));
        }

        [HttpGet]
        public IActionResult AddPromoCode()
        {
            AddNewPromoCodeViewModel model = new AddNewPromoCodeViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPromoCode(AddNewPromoCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await adminService.AddPromoCodeAsync(model);

            return RedirectToAction(nameof(ManagePromoCodes));
        }
    }
}
