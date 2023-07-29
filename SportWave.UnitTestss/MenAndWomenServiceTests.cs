using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.UnitTests
{
    public class MenAndWomenServiceTests
    {
        [Fact]
        public async Task GetProductsAsyncDoesntReturnNullWhenThereIsProduct()
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

            context.Dispose();
        }

        [Fact]
        public async Task GetProductsAsyncDoesntReturnNullWhenThereIsNoProduct()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IMenAndWomenService service = new MenAndWomenService(context);

            context.ProductGenders.Add(new ProductGender
            {
                Id = 1,
                Gender = "test"
            });
            await context.SaveChangesAsync();

            Assert.NotNull(service.GetProductsAsync(1));

            context.Dispose();
        }

        [Fact]
        public async Task GetFilteredProductsAsyncDoesntReturnNullWhenThereIsProduct()
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

            AllProductsViewModel model = new AllProductsViewModel();
            await context.SaveChangesAsync();

            Assert.NotNull(service.GetFilteredProductsAsync(1, model));

            context.Dispose();
        }

        [Fact]
        public async Task GetFilteredProductsAsyncDoesntReturnNullWhenThereIsNoProduct()
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

            AllProductsViewModel model = new AllProductsViewModel();
            await context.SaveChangesAsync();

            Assert.NotNull(service.GetFilteredProductsAsync(1, model));

            context.Dispose();
        }
    }
}
