using Microsoft.AspNetCore.Mvc;

namespace SportWave.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
