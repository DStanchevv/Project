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

        public async Task<IActionResult> Filter([FromForm]AllProductsViewModel model)
        {
           var viewModel = await menService.GetFilteredProductsAsync(1, model);
            return View(viewModel);
        }
    }
}
