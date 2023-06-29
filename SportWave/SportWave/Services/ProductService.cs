using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Services.Contracts;
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
