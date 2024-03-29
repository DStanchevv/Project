﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Controllers
{
    [AllowAnonymous]
    public class WomenController : Controller
    {
        private readonly IMenAndWomenService womenService;

        public WomenController(IMenAndWomenService womenService)
        {
            this.womenService = womenService;
        }

        public async Task<IActionResult> Women()
        {
            var model = await womenService.GetProductsAsync(2);
            if(model == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public async Task<IActionResult> Filter([FromForm] AllProductsViewModel model)
        {
            var viewModel = await womenService.GetFilteredProductsAsync(2, model);
            if (viewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}
