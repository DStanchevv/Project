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
            return await this.dbContext.Products.Where(p => p.Gender == "Male").Select(p => new MenViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImgUrl
            }).ToListAsync();
        }
    }
}
