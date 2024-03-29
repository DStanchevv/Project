﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.ShoppingCart;
using System.Security.Claims;

namespace SportWave.Controllers
{
    [Authorize(Roles = "Admin, User, Employee")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> ShoppingCart()
        {
            var model = await shoppingCartService.GetProductsInCartAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return View(model);
        }

        public async Task<IActionResult> Add(int id)
        {
            await shoppingCartService.AddQuantityToProductAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), id);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> Subtract(int id)
        {
            await shoppingCartService.SubtractQuantityToProductAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), id);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await shoppingCartService.RemoveProductFromCart(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), id);
            return RedirectToAction(nameof(ShoppingCart));
        }

        [HttpGet]
        public IActionResult AddPromoCode()
        {
            var model = new AddPromoCodeViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPromoCode(AddPromoCodeViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var successful = await shoppingCartService.ApplyDiscountAsync(model, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if(successful)
            {
                TempData["message"] = "Code applied successfully!";
            }
            else
            {
                TempData["message"] = "Code does not exsist or is invalid!";
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemovePromoCode()
        {
            var successful = await shoppingCartService.RemoveDiscountAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (successful)
            {
                TempData["message"] = "Code removed successfully!";
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
