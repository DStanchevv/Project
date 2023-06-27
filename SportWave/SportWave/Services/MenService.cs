using Microsoft.EntityFrameworkCore;
using SportWave.Data;
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

        public async Task<IEnumerable<MenViewModel>> GetMenProductsAsync()
        {
            return await this.dbContext.ProductsVariations.Where(p => p.Gender == "Male").Select(p => new MenViewModel
            {
                Name = p.Product.Name,
                Price = p.Product.Price,
                Description = p.Product.Description,
                ImageUrl = p.Product.ImgUrl,
                Color = p.Color
            }).ToListAsync();
        }
    }
}
