using Microsoft.AspNetCore.Mvc;

namespace SportWave.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
