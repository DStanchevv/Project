﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportWave.Services.Contracts;
using SportWave.ViewModels.CheckoutViewModels;
using System.Security.Claims;

namespace SportWave.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            this.checkoutService = checkoutService;
        }

        [HttpGet]
        public IActionResult PayWithCard()
        {
            PayWithCardViewModel model = new PayWithCardViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PayWithCard(PayWithCardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ExpiryDate < DateTime.Now)
            {
                model.Msg = "Card has expired!";
                return View(model);
            }

            foreach (var c in model.SecurityCode)
            {
                if (char.IsLetter(c))
                {
                    model.Msg = "Invalid security code!";
                    return View(model);
                }
            }

            await checkoutService.CheckoutWithCardAsync(model, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            await checkoutService.EmptyShoppingCart(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return RedirectToAction(nameof(OrderThanks));
        }

        [HttpGet]
        public IActionResult PayInCash()
        {
            PayInCashViewModel model = new PayInCashViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PayInCash(PayInCashViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await checkoutService.CheckoutWithCashAsync(model, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            
            await checkoutService.EmptyShoppingCart(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            
            return RedirectToAction(nameof(OrderThanks));
        }

        public IActionResult OrderThanks()
        {
            return View();
        }
    }
}