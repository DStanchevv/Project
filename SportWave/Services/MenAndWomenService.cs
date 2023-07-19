using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Services
{
    public class MenAndWomenService : IMenAndWomanService
    {
        private readonly SportWaveDbContext dbContext;

        public MenAndWomenService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllProductsViewModel> GetFilteredProductsAsync(int gender, AllProductsViewModel model)
        {
            var category = await dbContext.ProductCategories.Where(pc => pc.Id == model.CategoryId).Select(pc => pc.Category).FirstOrDefaultAsync();

            if (category != "All")
            {
                var products = await this.dbContext.Products.Where(p => p.GenderId == gender && p.CategoryId == model.CategoryId).Select(p => new MenAndWomenViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Color = p.Color,
                    Price = p.Price,
                    ImageUrl = p.ImgUrl
                }).ToListAsync();

                var categories = await dbContext.ProductCategories.Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Category = c.Category
                }).ToListAsync();

                var newModel = new AllProductsViewModel()
                {
                    Categories = categories,
                    Products = products
                };

                return newModel;
            }
            else
            {
                var products = await this.dbContext.Products.Where(p => p.GenderId == gender).Select(p => new MenAndWomenViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Color = p.Color,
                    Price = p.Price,
                    ImageUrl = p.ImgUrl
                }).ToListAsync();

                var categories = await dbContext.ProductCategories.Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Category = c.Category
                }).ToListAsync();

                var newModel = new AllProductsViewModel()
                {
                    Categories = categories,
                    Products = products
                };

                return newModel;
            }

        }

        public async Task<AllProductsViewModel> GetProductsAsync(int gender)
        {
            var products = await this.dbContext.Products.Where(p => p.GenderId == gender).Select(p => new MenAndWomenViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Color = p.Color,
                Price = p.Price,
                ImageUrl = p.ImgUrl
            }).ToListAsync();

            var categories = await dbContext.ProductCategories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Category = c.Category
            }).ToListAsync();

            var model = new AllProductsViewModel()
            {
                Categories = categories,
                Products = products
            };

            return model;
        }
    }
}
