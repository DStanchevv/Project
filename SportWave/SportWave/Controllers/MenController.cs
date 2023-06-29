﻿using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenViewModels;

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

        [HttpGet]
        public async Task<IActionResult> AddVariations()
        {
            AddVariationViewModel model = await menService.GetSizes();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddVariations(AddVariationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await menService.AddProductVariationAsync(model);

            return RedirectToAction(nameof(Men));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddProductViewModel model = await menService.GetNewAddedProductAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await menService.AddProductAsync(model);

            return RedirectToAction(nameof(AddVariations));
        }
    }
}
