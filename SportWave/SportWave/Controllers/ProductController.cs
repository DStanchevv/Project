using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;

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
    }
}
