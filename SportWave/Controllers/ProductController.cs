﻿using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetProductDetails(id);
            if (product == null)
            {
                TempData["message"] = "No such product!";
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<IActionResult> AddVariation(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                TempData["message"] = "No such product!";
                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        public async Task<IActionResult> AddVariation(int id, GetProductWithQuantityAndVariationsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await productService.GetProductByIdAsync(id);
                return View(model);
            }

            await productService.AddVariationToProductAsync(model, id);

            TempData["message"] = "Added Successfully!";
            return RedirectToAction("Details", "Product", new { Id = id });
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditProductViewModel product = await productService.GetProductByIdForEditAsync(id);

            if (product == null)
            {
                TempData["message"] = "No such product!";
                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.EditProductAsync(model, id);

            TempData["message"] = "Edited Successfully!";
            return RedirectToAction("Details", "Product", new { Id = id });
        }

        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await productService.GetProductByIdForRemoveAsync(id);
            
            if (product == null)
            {
                TempData["message"] = "No such product!";
                return RedirectToAction("Index", "Home");
            }
            
            var gender = product.Gender;

            await productService.RemoveProductAndVariationsAsync(product);
            TempData["message"] = "Removed Successfully!";

            if (gender == "Male")
            {
                return RedirectToAction("Men", "Men");
            }
            else
            {
                return RedirectToAction("Women", "Women");
            }
        }

        [Authorize(Roles = "Admin, User, Employee")]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromRoute] int id, [FromForm] CartProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Product", new { Id = id });
            }

            var availableQuantity = await productService.GetAvailableQuantityAsync(id, model);
            if (availableQuantity >= model.Quantity)
            {
                var product = await productService.GetProductByIdForCartAsync(id);

                if (product == null)
                {
                    TempData["message"] = "No such product!";
                    return RedirectToAction("Index", "Home");
                }

                product.Size = model.Size;
                product.Quantity = model.Quantity;

                await productService.AddToCartAsync(product, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                return RedirectToAction("ShoppingCart", "ShoppingCart");
            }
            else
            {
                TempData["message"] = "There aren't enough in stock!";
                return RedirectToAction("Details", "Product", new { Id = id });
            }
        }

        [Authorize(Roles = "Admin, User, Employee")]
        [HttpGet]
        public async Task<IActionResult> AddReview(int id)
        {
            AddAndEditReviewViewModel product = await productService.GetProductByIdForReviewAsync(id);

            if (product == null)
            {
                TempData["message"] = "No such product!";
                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }

        [Authorize(Roles = "Admin, User, Employee")]
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

        [Authorize(Roles = "Admin, User, Employee")]
        [HttpGet]
        public async Task<IActionResult> EditReview(int id)
        {
            AddAndEditReviewViewModel review = await productService.GetReviewByIdForEditReviewAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (review == null)
            {
                TempData["message"] = "No such review!";
                return RedirectToAction("Index", "Home");
            }

            return View(review);
        }

        [Authorize(Roles = "Admin, User, Employee")]
        [HttpPost]
        public async Task<IActionResult> EditReview(int id, AddAndEditReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.EditReviewAsync(model, id);

            TempData["message"] = "Edited Successfully!";
            return RedirectToAction("Details", "Product", new { Id = model.ProductId });
        }
    }
}
