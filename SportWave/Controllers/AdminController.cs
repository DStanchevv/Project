﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportWave.Services.Contracts;
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
            return RedirectToAction("Admin", "Admin");
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
    }
}
