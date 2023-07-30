using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using System.Security.Claims;

namespace SportWave.Controllers
{
    [Authorize(Roles = "Admin, User, Employee")]
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
            await userService.MarkedAsShippedAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            TempData["message"] = "Edited Successfully!";
            return RedirectToAction(nameof(Orders));
        }
    }
}
