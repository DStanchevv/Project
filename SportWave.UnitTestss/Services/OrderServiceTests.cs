using SportWave.Data.Models;
using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;
using Microsoft.EntityFrameworkCore;

namespace SportWave.UnitTestss.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task GetOrdersAsyncDoesNotReturnNullWhenThereIsOrder()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IOrderService service = new OrderService(context);

            context.Users.Add(new ApplicationUser
            {
                Id = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                UserName = "test@gmail.com",
                NormalizedUserName = "TEST@GMAIL.COM",
                Email = "test@gmail.com",
                NormalizedEmail = "TEST@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEHyipAH79RA/Wg+CUvmeFxVsOIm2zlnApqzSeSEtHgKPARYIni9m+EyBMv5XjsGq5Q==",
                SecurityStamp = "34CRR52GZU54BMGOKQLXNSZ3TE5VI7ZT",
                ConcurrencyStamp = "88a2c0c0-8884-4045-a4e0-154b1d1cd30d",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });

            context.ProductCategories.Add(new ProductCategory
            {
                Id = 1,
                Category = "test"
            });

            context.ProductGenders.Add(new ProductGender
            {
                Id = 1,
                Gender = "test"
            });

            context.Products.Add(new Product
            {
                Id = 1,
                Name = "Test",
                Price = 30m,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("29F8F073-D44D-4A8F-B233-05A2243BE36A"),
                Country = "Test",
                Town = "Test",
                StreetName = "Test",
                StreetNumber = 48,
                AdditionalInfo = ""
            });

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "Stripe"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("735ABF06-248B-40C5-BFD2-907552E1C50A"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                ShippingAddressId = Guid.Parse("29F8F073-D44D-4A8F-B233-05A2243BE36A"),
                DateOfOrder = DateTime.Now,
                Status = "test",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                PaymentMethodId = Guid.Parse("735ABF06-248B-40C5-BFD2-907552E1C50A")
            });

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetOrdersAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetOrdersAsyncDoesNotReturnNullWhenThereIsNoOrder()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IOrderService service = new OrderService(context);

            context.Users.Add(new ApplicationUser
            {
                Id = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                UserName = "test@gmail.com",
                NormalizedUserName = "TEST@GMAIL.COM",
                Email = "test@gmail.com",
                NormalizedEmail = "TEST@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEHyipAH79RA/Wg+CUvmeFxVsOIm2zlnApqzSeSEtHgKPARYIni9m+EyBMv5XjsGq5Q==",
                SecurityStamp = "34CRR52GZU54BMGOKQLXNSZ3TE5VI7ZT",
                ConcurrencyStamp = "88a2c0c0-8884-4045-a4e0-154b1d1cd30d",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });

            await context.SaveChangesAsync();

            var result = await service.GetOrdersAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task MarkedAsShippedAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IOrderService service = new OrderService(context);

            context.Users.Add(new ApplicationUser
            {
                Id = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                UserName = "test@gmail.com",
                NormalizedUserName = "TEST@GMAIL.COM",
                Email = "test@gmail.com",
                NormalizedEmail = "TEST@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEHyipAH79RA/Wg+CUvmeFxVsOIm2zlnApqzSeSEtHgKPARYIni9m+EyBMv5XjsGq5Q==",
                SecurityStamp = "34CRR52GZU54BMGOKQLXNSZ3TE5VI7ZT",
                ConcurrencyStamp = "88a2c0c0-8884-4045-a4e0-154b1d1cd30d",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                ShippingAddressId = Guid.Parse("29F8F073-D44D-4A8F-B233-05A2243BE36A"),
                DateOfOrder = DateTime.Now,
                Status = "test",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                PaymentMethodId = Guid.Parse("735ABF06-248B-40C5-BFD2-907552E1C50A")
            });

            context.OrderStatuses.Add(new OrderStatus
            {
                Status = "Shipped"
            });


            await context.SaveChangesAsync();

            var status = await context.Orders.Where(o => o.Id == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(o => o.Status).FirstOrDefaultAsync();

            await service.MarkedAsShippedAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            status = await context.Orders.Where(o => o.Id == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(o => o.Status).FirstOrDefaultAsync();

            Assert.Equal("Shipped", status);

            context.Dispose();
        }
    }
}
