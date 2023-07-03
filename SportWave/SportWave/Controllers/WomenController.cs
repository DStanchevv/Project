﻿using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Controllers
{
    public class WomenController : Controller
    {
        private readonly IMenAndWomanService womenService;

        public WomenController(IMenAndWomanService womenService)
        {
            this.womenService = womenService;
        }

        public async Task<IActionResult> Women()
        {
            var model = await womenService.GetProductsAsync(2);
            return View(model);
        }
    }
}
