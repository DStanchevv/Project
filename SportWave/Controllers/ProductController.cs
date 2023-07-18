using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.ProductViewModels;
using SportWave.ViewModels.ShoppingCart;
using System.Security.Claims;

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
                return RedirectToAction("Home", "Index");
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

            return RedirectToAction("Details", "Product", new { Id = id});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditProductViewModel product = await productService.GetProductByIdForEditAsync(id);

            if(product == null)
            {
                return RedirectToAction("Home", "Index");
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.EditProductAsync(model, id);

            return RedirectToAction("Details", "Product", new { Id = id });
        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await productService.GetProductByIdForRemoveAsync(id);
            var gender = product.Gender;

            if(product == null)
            {
                return RedirectToAction("Home", "Index");
            }

            await productService.RemoveProductAndVariationsAsync(product);

            if (gender == "Male")
            {
                return RedirectToAction("Men", "Men");
            }
            else
            {
                return RedirectToAction("Women", "Women");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromRoute]int id, [FromForm]CartProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Product", new { Id = id });
            }

            var availableQuantity = await productService.GetAvailableQuantityAsync(id, model);
            if (availableQuantity >= model.Quantity)
            {
                var product = await productService.GetProductByIdForCartAsync(id);

                if (product == null)
                {
                    return RedirectToAction("Home", "Index");
                }

                product.Size = model.Size;
                product.Quantity = model.Quantity;

                await productService.AddToCartAsync(product, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                return RedirectToAction("ShoppingCart", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Details", "Product", new { Id = id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddReview(int id)
        {
            AddAndEditReviewViewModel product = await productService.GetProductByIdForReviewAsync(id);

            if (product == null)
            {
                return RedirectToAction("Home", "Index");
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int id, AddAndEditReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.AddReviewAsync(model, id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return RedirectToAction("Details", "Product", new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> EditReview(int id)
        {
            AddAndEditReviewViewModel review = await productService.GetReviewByIdForEditReviewAsync(id);

            if (review == null)
            {
                return RedirectToAction("Home", "Index");
            }

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> EditReview(int id, AddAndEditReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.EditReviewAsync(model, id);

            return RedirectToAction("Details", "Product", new { Id = model.ProductId });
        }
    }
}
