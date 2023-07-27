using Microsoft.AspNetCore.Authorization;
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
            PayInCashViewModel model = new PayInCashViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Order(PayInCashViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isSuccessful = await checkoutService.CheckoutWithCashAsync(model, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (isSuccessful)
            {
                return RedirectToAction(nameof(CreateCheckoutSession));
            }
            else
            {
                model.Msg = "Invalid information or cart is empty!";
                return View(model);
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
