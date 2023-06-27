using Microsoft.AspNetCore.Mvc;

namespace SportWave.Controllers
{
    public class WomenController : Controller
    {
        public IActionResult Women()
        {
            return View();
        }
    }
}
