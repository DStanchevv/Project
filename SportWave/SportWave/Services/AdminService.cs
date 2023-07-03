using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;
using SportWave.ViewModels.ProductViewModels;

namespace SportWave.Services
{
    public class AdminService : IAdminService
    {

        private readonly SportWaveDbContext dbContext;

        public AdminService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddProductAsync(AddProductViewModel model)
        {

            Product product = new Product()
            {
                Name = model.Name,
                Price = decimal.Parse(model.Price),
                Description = model.Description,
                CategoryId = model.CategoryId,
                Color = model.Color,
                GenderId = model.GenderId,
                ImgUrl = model.ImgUrl
            };

            if (!dbContext.Products.Any(p => p.Name == product.Name && p.Color == product.Color && p.GenderId == product.GenderId))
            {
                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<AddProductViewModel> GetNewAddedProductAsync()
        {
            var categories = await dbContext.ProductCategories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Category = c.Category
            }).ToListAsync();

            var genders = await dbContext.ProductGenders.Select(g => new GenderViewModel
            {
                Id = g.Id,
                Gender = g.Gender
            }).ToListAsync();

            var model = new AddProductViewModel
            {
                Categories = categories,
                Genders = genders
            };

            return model;
        }
    }
}
