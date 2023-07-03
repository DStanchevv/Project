using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Controllers
{
    public class MenController : Controller
    {
        private readonly IMenAndWomanService menService;

        public MenController(IMenAndWomanService menService)
        {
            this.menService = menService;
        }

        public async Task<IActionResult> Men()
        {
            var model = await menService.GetProductsAsync(1);
            return View(model);
        }
    }
}
