using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;

namespace SportWave.Controllers
{
    public class MenController : Controller
    {
        private readonly IMenService menService;

        public MenController(IMenService menService)
        {
            this.menService = menService;
        }

        public async Task<IActionResult> Men()
        {
            var model = await menService.GetMenProductsAsync();
            return View(model);
        }
    }
}
