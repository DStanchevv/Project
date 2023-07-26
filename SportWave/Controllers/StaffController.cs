using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services;
using SportWave.Services.Contracts;
using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class StaffController : Controller
    {
        private readonly IStaffService staffService;

        public StaffController(IStaffService adminService)
        {
            this.staffService = adminService;
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
                return View(model);
            }

            await staffService.AddProductAsync(model);

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

            return RedirectToAction("Staff", "Staff");
        }

        public async Task<IActionResult> ManageOrders()
        {
            var model = await staffService.GetOrdersAsync();

            if(model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> SendOrder(Guid id)
        {
            await staffService.SendOrderAsync(id);

            return RedirectToAction(nameof(ManageOrders));
        }

        public async Task<IActionResult> ClearOrder(Guid id)
        {
            await staffService.ClearOrderAsync(id);

            return RedirectToAction(nameof(ManageOrders));
        }

        [HttpPost]
        public async Task<IActionResult> FilterOrders([FromForm] ManageOrdersViewModel model)
        {
            var viewModel = await staffService.GetFilteredOrdersAsync(model);
            if(viewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> ManagePromoCodes()
        {
            var model = await staffService.GetPromoCodesAsync();

            if(model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> MakeInvalid([FromRoute]Guid id)
        {
            var promoCode = await staffService.GetPromoCodeToChangeStatusAsync(id);

            if(promoCode == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            await staffService.MakeInvalidAsync(promoCode);
            
            return RedirectToAction(nameof(ManagePromoCodes));
        }

        public async Task<IActionResult> MakeValid([FromRoute]Guid id)
        {
            var promoCode = await staffService.GetPromoCodeToChangeStatusAsync(id);

            if(promoCode == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await staffService.MakeValidAsync(promoCode);

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

            return RedirectToAction(nameof(ManagePromoCodes));
        }
    }
}
