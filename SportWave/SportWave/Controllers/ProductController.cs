using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenViewModels;
using SportWave.ViewModels.ProductViewModels;

namespace SportWave.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetProductDetails(id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> AddVariation(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if(product == null)
            {
                return RedirectToAction("Men", "Men");
            }
            
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddVariation(int id, GetProductWithQuantityAndVariationsViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.AddVariationToProductAsync(model, id);

            return RedirectToAction("Men", "Men");
        }
    }
}
