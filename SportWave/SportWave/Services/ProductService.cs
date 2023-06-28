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
            var colors = await dbContext.ProductColors.Select(c => new ColorsViewModel
            {
                Color = c.Color
            }).OrderBy(c => c.Color).ToListAsync();
            
            var sizes = await dbContext.ProductSizes.Select(s => new SizesViewModel
            {
                Size = s.Size
            }).ToListAsync();

            var variations = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id).Select(pv => new ProductVariationModel
            {
                Size = pv.ProductSize.Size,
                Color = pv.ProductColor.Color,
                Quantity = pv.Quantity
            }).ToListAsync();
            
            return await dbContext.Products.Where(p => p.Id == id).Select(p => new ProductDetailsViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImgUrl,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category.Category,
                Colors = colors,
                Sizes = sizes,
                ProductVariations = variations
            }).FirstOrDefaultAsync();
        }
    }
}
