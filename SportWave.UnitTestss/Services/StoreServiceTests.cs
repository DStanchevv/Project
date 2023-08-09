using SportWave.Data.Models;
using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;

namespace SportWave.UnitTestss.Services
{
    public class StoreServiceTests
    {
        [Fact]
        public async Task GetStoresReturnsNotEmptyListWhenThereAreStores()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            context.Stores.Add(new Store
            {
                Id = 1,
                Country = "Test",
                Region = "Test",
                City = "Test",
                Location = "Test"
            });

            await context.SaveChangesAsync();

            var res1 = await service.GetAllStoresAsync();

            Assert.NotEmpty(res1);

            context.Dispose();
        }

        [Fact]
        public async Task GetStoresReturnsEmptyListWhenThereAreNoStores()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            var res1 = await service.GetAllStoresAsync();

            Assert.Empty(res1);

            context.Dispose();
        }

        [Fact]
        public async Task GetStoresDoesNotReturnNullWhenThereAreStores()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            context.Stores.Add(new Store
            {
                Id = 1,
                Country = "Test",
                Region = "Test",
                City = "Test",
                Location = "Test"
            });

            await context.SaveChangesAsync();

            var res1 = await service.GetAllStoresAsync();

            Assert.NotNull(res1);

            context.Dispose();
        }

        [Fact]
        public async Task GetStoresDoesNotReturnNullWhenThereAreNoStores()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            var res1 = await service.GetAllStoresAsync();

            Assert.NotNull(res1);

            context.Dispose();
        }

        [Fact]
        public async Task GetStoreByCityOrRegionWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            context.Stores.Add(new Store
            {
                Id = 1,
                Country = "Test",
                Region = "Test",
                City = "Test",
                Location = "Test"
            });

            await context.SaveChangesAsync();

            var res1 = await service.GetStoreByCityOrRegionAsync("Test", "Test");

            Assert.Equal(res1.Stores.Count(), 1);

            context.Dispose();
        }

        [Fact]
        public async Task GetStoreByCityOrRegionWorksProperlyWhenThereAreNoStores()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            var res1 = await service.GetStoreByCityOrRegionAsync("Test", "Test");

            Assert.Equal(res1.Stores.Count(), 0);

            context.Dispose();
        }

        [Fact]
        public async Task GetStoreByCityOrRegionDoesNotReturnNullWhenThereAreStores()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            context.Stores.Add(new Store
            {
                Id = 1,
                Country = "Test",
                Region = "Test",
                City = "Test",
                Location = "Test"
            });

            await context.SaveChangesAsync();

            var res1 = await service.GetStoreByCityOrRegionAsync("Test", "Test");

            Assert.NotNull(res1);

            context.Dispose();
        }

        [Fact]
        public async Task GetStoreByCityOrRegionDoesNotReturnNullWhenThereAreNoStores()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IStoreService service = new StoreService(context);

            var res1 = await service.GetStoreByCityOrRegionAsync("Test", "Test");

            Assert.NotNull(res1);

            context.Dispose();
        }
    }
}
