using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenViewModels;
using SportWave.ViewModels.ProductViewModels;

namespace SportWave.Services
{
    public class ProductService : IProductService
    {
        private readonly SportWaveDbContext dbContext;
        public ProductService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddVariationToProductAsync(GetProductWithQuantityAndVariationsViewModel model, int id)
        {
            var sizes = await dbContext.ProductSizes.Select(s => new SizesViewModel
            {
                SizeId = s.Id,
                Size = s.Size
            }).ToListAsync();
            model.Sizes = sizes;

            var variations = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id).Select(pv => new ProductVariationModel
            {
                ProductId = pv.ProductId,
                SizeId = pv.ProductSize.Id,
                Quantity = pv.Quantity
            }).ToListAsync();
            model.ProductVariations = variations;

            foreach (var size in model.Sizes)
            {
                if (model.ProductVariations.Any(pv => pv.SizeId == size.SizeId))
                {
                    var productVariation = await dbContext.ProductsVariations.Where(pv => pv.SizeId == size.SizeId).FirstAsync();
                    productVariation.Quantity += model.Quantity;
                }
                else
                {
                    
                    ProductVariation var = new ProductVariation()
                    {
                        ProductId = model.Id,
                        SizeId = size.SizeId,
                        Quantity = model.Quantity
                    };

                    await dbContext.ProductsVariations.AddAsync(var);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdAsync(int id)
        {
            return await dbContext.Products.Where(p => p.Id == id).Select(p => new GetProductWithQuantityAndVariationsViewModel
            {
                Id = p.Id,
            }).FirstOrDefaultAsync();
        }

        public async Task<ProductDetailsViewModel> GetProductDetails(int id)
        {
            var sizes = await dbContext.ProductSizes.Select(s => new SizesViewModel
            {
                Size = s.Size
            }).ToListAsync();

            var variations = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id).Select(pv => new ProductVariationModel
            {
                Size = pv.ProductSize.Size,
                Quantity = pv.Quantity
            }).ToListAsync();

            return await dbContext.Products.Where(p => p.Id == id).Select(p => new ProductDetailsViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImgUrl,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Color = p.Color,
                Category = p.Category.Category,
                Sizes = sizes,
                ProductVariations = variations
            }).FirstOrDefaultAsync();
        }
    }
}
