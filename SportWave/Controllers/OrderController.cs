using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using System.Security.Claims;

namespace SportWave.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService userService;

        public OrderController(IOrderService orderService)
        {
            this.userService = orderService;
        }

        public async Task<IActionResult> Orders()
        {
            var model = await userService.GetOrdersAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View(model);
        }

        public async Task<IActionResult> MarkAsShipped(Guid id)
        {
            await userService.MarkedAsShippedAsync(id);

            return RedirectToAction(nameof(Orders));
        }
    }
}
