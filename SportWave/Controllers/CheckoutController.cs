﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.CheckoutViewModels;
using System.Security.Claims;

namespace SportWave.Controllers
{
    [Authorize(Roles = "User, Admin, Employee")]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService checkoutService;
        private readonly IStripeCheckoutService stripeCheckoutService;

        public CheckoutController(ICheckoutService checkoutService, IStripeCheckoutService stripeCheckoutService)
        {
            this.checkoutService = checkoutService;
            this.stripeCheckoutService = stripeCheckoutService;
        }

        [HttpGet]
        public IActionResult Order()
        {
            PlaceOrderViewModel model = new PlaceOrderViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Order(PlaceOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccessful = await checkoutService.PlaceOrderAsync(model, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (isSuccessful)
            {
                return RedirectToAction(nameof(CreateCheckoutSession));
            }
            else
            {
                TempData["message"] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

        }

        public async Task<IActionResult> CreateCheckoutSession()
        {
            string url = await stripeCheckoutService.CheckoutSessionAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))); 
            return Redirect(url); 
        }

        public async Task<IActionResult> OrderThanks()
        {
            await checkoutService.EmptyShoppingCart(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return View();
        }
    }
}
