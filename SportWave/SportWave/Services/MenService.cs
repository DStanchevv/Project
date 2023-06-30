using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenViewModels;
using SportWave.ViewModels.ProductViewModels;

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
                Color = model.Color,
                Gender = "Male",
                ImgUrl = model.ImgUrl
            };

            if (!dbContext.Products.Any(p => p.Name == product.Name && p.Color == product.Color))
            {
                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddProductVariationAsync(AddVariationViewModel model)
        {
            var Ids = await dbContext.Products.Select(p => p.Id).ToListAsync();
            model.ProductId = Ids.Last();

            var sizes = await dbContext.ProductSizes.Select(s => new SizesViewModel
            {
                SizeId = s.Id,
                Size = s.Size
            }).ToListAsync();
            model.Sizes = sizes;

            foreach (var size in model.Sizes)
            {

                ProductVariation productVariation = new ProductVariation()
                {
                    ProductId = model.ProductId,
                    SizeId = size.SizeId,
                    Quantity = model.Quantity
                };

                await dbContext.ProductsVariations.AddAsync(productVariation);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenViewModel>> GetMenProductsAsync()
        {
            return await this.dbContext.Products.Where(p => p.Gender == "Male").Select(p => new MenViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Color = p.Color,
                Price = p.Price,
                ImageUrl = p.ImgUrl
            }).ToListAsync();
        }

        public async Task<AddProductViewModel> GetNewAddedProductAsync()
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
