using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;
using SportWave.ViewModels.StaffViewModels;

namespace SportWave.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class StaffController : Controller
    {
        private readonly IStaffService staffService;
        private readonly IPhotoService photoService;

        public StaffController(IStaffService adminService, IPhotoService photoService)
        {
            this.staffService = adminService;
            this.photoService = photoService;
        }

        public IActionResult Staff()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddProductViewModel model = await staffService.GetNewAddedProductAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await staffService.GetNewAddedProductAsync();
                return View(model);
            }

            var result = await photoService.AddPhotoAsync(model.ImgUrl);
            if (result != null)
            {
                await staffService.AddProductAsync(model, result.Url.ToString());
                TempData["message"] = "Added Successfully!";
            }
            else
            {
                TempData["message"] = "Invalid image!";
                model = await staffService.GetNewAddedProductAsync();
                return View(model);
            }

            if (model.Gender == "Male")
            {
                return RedirectToAction("Men", "Men");
            }
            else
            {
                return RedirectToAction("Women", "Women");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> MakeUserEmployee()
        {
            MakeUserEmployeeViewModel model = await staffService.GetAdminAndEmployeeEmailsAsync();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> MakeUserEmployee(MakeUserEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await staffService.MakeUserEmployeeAsync(model);

            TempData["message"] = "Added Successfully!";
            return RedirectToAction(nameof(MakeUserEmployee));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveEmployee(Guid id)
        {
            var employee = await staffService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return RedirectToAction(nameof(MakeUserEmployee));
            }

            await staffService.RemoveEmployeeAsync(employee);

            TempData["message"] = "Removed Successfully!";
            return RedirectToAction(nameof(MakeUserEmployee));
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

            await staffService.AddCategoryAsync(model);

            TempData["message"] = "Added Successfully!";
            return RedirectToAction("Staff", "Staff");
        }

        public async Task<IActionResult> ManageOrders()
        {
            var model = await staffService.GetOrdersAsync();

            if (model == null)
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> SendOrder(Guid id)
        {
            await staffService.SendOrderAsync(id);

            TempData["message"] = "Sent Successfully!";
            return RedirectToAction(nameof(ManageOrders));
        }

        public async Task<IActionResult> ClearOrder(Guid id)
        {
            await staffService.ClearOrderAsync(id);

            TempData["message"] = "Cleared Successfully!";
            return RedirectToAction(nameof(ManageOrders));
        }

        [HttpPost]
        public async Task<IActionResult> FilterOrders([FromForm] ManageOrdersViewModel model)
        {
            var viewModel = await staffService.GetFilteredOrdersAsync(model);
            if (viewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> ManagePromoCodes()
        {
            var model = await staffService.GetPromoCodesAsync();

            if (model == null)
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> MakeInvalid([FromRoute] Guid id)
        {
            var promoCode = await staffService.GetPromoCodeToChangeStatusAsync(id);

            if (promoCode == null)
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

            await staffService.MakeInvalidAsync(promoCode);

            TempData["message"] = "Edited Successfully!";
            return RedirectToAction(nameof(ManagePromoCodes));
        }

        public async Task<IActionResult> MakeValid([FromRoute] Guid id)
        {
            var promoCode = await staffService.GetPromoCodeToChangeStatusAsync(id);

            if (promoCode == null)
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

            await staffService.MakeValidAsync(promoCode);

            TempData["message"] = "Edited Successfully!";
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

            await staffService.AddPromoCodeAsync(model);

            TempData["message"] = "Added Successfully!";
            return RedirectToAction(nameof(ManagePromoCodes));
        }

        [HttpGet]
        public IActionResult AddStore()
        {
            AddStoreViewModel model = new AddStoreViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStore(AddStoreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await staffService.AddStoreAsync(model);

            TempData["message"] = "Added Successfully!";
            return RedirectToAction(nameof(ManageStores));
        }

        public async Task<IActionResult> ManageStores()
        {
            var model = await staffService.GetStoresAsync();

            if (model == null)
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> RemoveStore([FromRoute] int id)
        {
            var successful = await staffService.RemoveStoreAsync(id);

            if (successful)
            {
                TempData["message"] = "Removed Successfully!";
            }
            else
            {
                TempData["message"] = "Something went wrong!";
            }

            return RedirectToAction(nameof(ManageStores));
        }
    }
}
