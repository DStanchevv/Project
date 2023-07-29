using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services;
using SportWave.Services.Contracts;
using Xunit;
using Assert = Xunit.Assert;

namespace SportWave.UnitTests
{
    public class MenAndWomenServiceTests
    {
        [Fact]
        public async Task GetProductsAsyncDoesntReturnNull()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IMenAndWomenService service = new MenAndWomenService(context);

            context.ProductGenders.Add(new ProductGender
            {
                Id = 1,
                Gender = "test"
            });

            context.ProductCategories.Add(new ProductCategory
            {
                Id = 1,
                Category = "test"
            });

            context.Products.Add(new Product
            {
                Id = 1,
                Name = "test",
                Color = "test",
                Price = 30m,
                Description = "test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                GenderId = 1,
                ImgUrl = "test"
            });
            await context.SaveChangesAsync();

            Assert.NotNull(service.GetProductsAsync(1));
        }

        
    }
}
