using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenViewModels;

namespace SportWave.Services
{
    public class MenService : IMenService
    {
        private readonly SportWaveDbContext dbContext;

        public MenService(SportWaveDbContext dbContext)
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
                Gender = "Male",
                ImgUrl = model.ImgUrl
            };

            if(!dbContext.Products.Any(p => p.Name == product.Name))
            {
                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MenViewModel>> GetMenProductsAsync()
        {
            return await this.dbContext.Products.Where(p => p.Gender == "Male").Select(p => new MenViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImgUrl
            }).ToListAsync();
        }

        public async Task<AddProductViewModel> GetNewAddProductAsync()
        {
            var categories = await dbContext.ProductCategories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Category = c.Category
            }).ToListAsync();

            var model = new AddProductViewModel
            {
                Categories = categories
            };

            return model;
        }
    }
}
