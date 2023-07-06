﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditProductViewModel product = await productService.GetProductByIdForEditAsync(id);

            if(product == null)
            {
                return RedirectToAction("Men", "Men");
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

            return RedirectToAction("Men", "Men");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await productService.GetProductByIdForRemoveAsync(id);

            if(product == null)
            {
                return RedirectToAction("Men", "Men");
            }

            await productService.RemoveProductAndVariationsAsync(product);

            return RedirectToAction("Men", "Men");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromRoute]int id, [FromForm]CartProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Product", new { Id = id });
            }

            var product = await productService.GetProductByIdForCartAsync(id);

            if (product == null)
            {
                return RedirectToAction("Men", "Men");
            }

            product.Size = model.Size;
            product.Quantity = model.Quantity;

            await productService.AddToCartAsync(product, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return RedirectToAction("Men", "Men");
        }
    }
}