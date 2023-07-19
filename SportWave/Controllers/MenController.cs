using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Controllers
{
    public class MenController : Controller
    {
        private readonly IMenAndWomenService menService;

        public MenController(IMenAndWomenService menService)
        {
            this.menService = menService;
        }

        public async Task<IActionResult> Men()
        {
            var model = await menService.GetProductsAsync(1);
            if(model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public async Task<IActionResult> Filter([FromForm]AllProductsViewModel model)
        {
           var viewModel = await menService.GetFilteredProductsAsync(1, model);
            if (viewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}
