using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;
using SportWave.UnitTests.FakeClasses;
using SportWave.ViewModels.MenAndWomenViewModels;
using Microsoft.EntityFrameworkCore;
using SportWave.Data.Models;
using Microsoft.AspNetCore.Http;
using SportWave.ViewModels.AdminViewModels;
using System.Linq.Expressions;
using Moq;
using Microsoft.AspNetCore.Identity;

namespace SportWave.UnitTestss.Services
{
    public class StaffServiceTests
    {
        [Fact]
        public async Task AddCategoryAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            AddCategoryViewModel model = new AddCategoryViewModel()
            {
                Name = "test"
            };

            await service.AddCategoryAsync(model);
            var result = await context.ProductCategories.ToListAsync();

            Assert.NotEmpty(result);

            context.Dispose();
        }

        [Fact]
        public async Task AddCategoryAsyncDoesNotAddCattegoryIfItExists()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            context.ProductCategories.Add(new ProductCategory
            {
                Id = 1,
                Category = "test"
            });

            await context.SaveChangesAsync();

            AddCategoryViewModel model = new AddCategoryViewModel()
            {
                Name = "test"
            };

            var res1 = await context.ProductCategories.ToListAsync();
            await service.AddCategoryAsync(model);
            var res2 = await context.ProductCategories.ToListAsync();

            Assert.Equal(res1.Count(), res2.Count());

            context.Dispose();
        }

        [Fact]
        public async Task AddProductAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            await context.SaveChangesAsync();

            AddProductViewModel model = new AddProductViewModel()
            {
                Name = "test",
                Price = "30",
                CategoryId = 1,
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test",
                GenderId = 1,
                ImgUrl = new FormFile(new MemoryStream(), 0, 1, "t", "t.img"),
                Gender = "test"
            };

            await service.AddProductAsync(model, "test");
            var result = await context.Products.ToListAsync();

            Assert.NotEmpty(result);

            context.Dispose();
        }

        [Fact]
        public async Task AddProductAsyncDoesNotAddProductIfItExists()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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
                Name = "test",
                Price = 30m,
                CategoryId = 1,
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test",
                GenderId = 1,
                ImgUrl = "test",
                Id = 1
            });

            await context.SaveChangesAsync();

            AddProductViewModel model = new AddProductViewModel()
            {
                Name = "test",
                Price = "30",
                CategoryId = 1,
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test",
                GenderId = 1,
                ImgUrl = new FormFile(new MemoryStream(), 0, 1, "t", "t.img"),
                Gender = "test"
            };

            var res1 = await context.Products.ToListAsync();
            await service.AddProductAsync(model, "test");
            var res2 = await context.Products.ToListAsync();

            Assert.Equal(res1.Count(), res2.Count());

            context.Dispose();
        }

        [Fact]
        public async Task AddPromoCodeAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            AddNewPromoCodeViewModel model = new AddNewPromoCodeViewModel()
            {
                Code = "test",
                Value = 10
            };

            await service.AddPromoCodeAsync(model);
            var result = await context.PromoCodes.ToListAsync();

            Assert.NotEmpty(result);

            context.Dispose();
        }

        [Fact]
        public async Task AddPromoCodeAsyncDoesNotAddPromoCodeIfItExists()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("3D591835-0C9A-4257-8AD6-026131DFFC31"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            await context.SaveChangesAsync();

            AddNewPromoCodeViewModel model = new AddNewPromoCodeViewModel()
            {
                Code = "test",
                Value = 10
            };

            var res1 = await context.PromoCodes.ToListAsync();
            await service.AddPromoCodeAsync(model);
            var res2 = await context.PromoCodes.ToListAsync();

            Assert.Equal(res1.Count, res2.Count);

            context.Dispose();
        }

        [Fact]
        public async Task ClearOrderAsyncClearsOrders()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "test"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860"),
                Country = "test",
                Town = "test",
                StreetName = "test",
                StreetNumber = 48
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                PaymentMethodId = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                DateOfOrder = DateTime.Now,
                Status = "Shipped",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                ShippingAddressId = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860")
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

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            await service.ClearOrderAsync(Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"));

            var result = await context.Orders.ToListAsync();

            Assert.Empty(result);

            context.Dispose();
        }

        [Fact]
        public async Task ClearOrderAsyncClearsOrderProducts()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "test"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860"),
                Country = "test",
                Town = "test",
                StreetName = "test",
                StreetNumber = 48
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                PaymentMethodId = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                DateOfOrder = DateTime.Now,
                Status = "Shipped",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                ShippingAddressId = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860")
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

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            await service.ClearOrderAsync(Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"));

            var result = await context.ProductsOrders.ToListAsync();

            Assert.Empty(result);

            context.Dispose();
        }

        [Fact]
        public async Task ClearOrderAsyncDoesNotClearOrderIfStatusIsNotShipped()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "test"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860"),
                Country = "test",
                Town = "test",
                StreetName = "test",
                StreetNumber = 48
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                PaymentMethodId = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                DateOfOrder = DateTime.Now,
                Status = "test",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                ShippingAddressId = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860")
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

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            await service.ClearOrderAsync(Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"));

            var result = await context.Orders.ToListAsync();

            Assert.NotEmpty(result);

            context.Dispose();
        }

        [Fact]
        public async Task ClearOrderAsyncDoesNotClearOrderProductsIfStatusIsNotShipped()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "test"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860"),
                Country = "test",
                Town = "test",
                StreetName = "test",
                StreetNumber = 48
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                PaymentMethodId = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                DateOfOrder = DateTime.Now,
                Status = "test",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                ShippingAddressId = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860")
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

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            await service.ClearOrderAsync(Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"));

            var result = await context.ProductsOrders.ToListAsync();

            Assert.NotEmpty(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetAdminAndEmployeeEmailsAsyncDoesNotReturnNullWhenThereAreNoUsersInRoles()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            var result = service.GetAdminAndEmployeeEmailsAsync();

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetFilteredOrdersAsyncDoesNotReturnNulls()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "test"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860"),
                Country = "test",
                Town = "test",
                StreetName = "test",
                StreetNumber = 48
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                PaymentMethodId = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                DateOfOrder = DateTime.Now,
                Status = "test",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                ShippingAddressId = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860")
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

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            ManageOrdersViewModel model = new ManageOrdersViewModel();

            var result = await service.GetFilteredOrdersAsync(model);

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetNewAddedProductAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            var result = await service.GetNewAddedProductAsync();

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetOrdersAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "test"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860"),
                Country = "test",
                Town = "test",
                StreetName = "test",
                StreetNumber = 48
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                PaymentMethodId = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                DateOfOrder = DateTime.Now,
                Status = "test",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                ShippingAddressId = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860")
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

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetOrdersAsync();

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetPromoCodesAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            await context.SaveChangesAsync();

            var result = await service.GetPromoCodesAsync();

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetPromoCodeToChangeStatusAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            await context.SaveChangesAsync();

            var result = await service.GetPromoCodeToChangeStatusAsync(Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task MakeInvalidAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            await context.SaveChangesAsync();

            PromoCodesViewModel model = new PromoCodesViewModel()
            {
                Code = "test",
                CodeId = Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6"),
                Value = 10,
                IsValid = true
            };

            await service.MakeInvalidAsync(model);

            var result = await context.PromoCodes.Where(pc => pc.Id == Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6")).Select(pc => pc.isValid).FirstOrDefaultAsync();

            Assert.Equal(false, result);

            context.Dispose();
        }

        [Fact]
        public async Task MakeValidAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            await context.SaveChangesAsync();

            PromoCodesViewModel model = new PromoCodesViewModel()
            {
                Code = "test",
                CodeId = Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6"),
                Value = 10,
                IsValid = true
            };

            await service.MakeValidAsync(model);

            var result = await context.PromoCodes.Where(pc => pc.Id == Guid.Parse("1953B72D-70C2-4DDC-9D35-4C448D5E8AB6")).Select(pc => pc.isValid).FirstOrDefaultAsync();

            Assert.Equal(true, result);

            context.Dispose();
        }

        [Fact]
        public async Task SendOrderAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "test"
            });

            context.UsersPaymentMethods.Add(new UserPaymentMethod
            {
                Id = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                PaymentTypeId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            context.Addresses.Add(new Address
            {
                Id = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860"),
                Country = "test",
                Town = "test",
                StreetName = "test",
                StreetNumber = 48
            });

            context.Orders.Add(new Order
            {
                Id = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                PaymentMethodId = Guid.Parse("6CF78EAC-3EE4-44E9-B497-A6CA163BEA90"),
                DateOfOrder = DateTime.Now,
                Status = "test",
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                OrderTotal = 30m,
                ShippingAddressId = Guid.Parse("061BB136-759F-44F4-A0D5-4DDF2BC7A860")
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

            context.ProductsOrders.Add(new ProductOrder
            {
                OrderId = Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            context.OrderStatuses.Add(new OrderStatus
            {
                Status = "On the way"
            });

            await context.SaveChangesAsync();

            await service.SendOrderAsync(Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45"));

            var result = await context.Orders.Where(pc => pc.Id == Guid.Parse("06857C81-5766-4924-9D71-13788CCF9A45")).Select(pc => pc.Status).FirstOrDefaultAsync();

            Assert.Equal("On the way", result);

            context.Dispose();
        }

        [Fact]
        public async Task GetEmployeeByIdAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            var result = await service.GetEmployeeByIdAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveEmployeeAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            FakeUserManager userManager = new FakeUserManager(context);
            IStaffService service = new StaffService(context, userManager);

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

            context.Roles.Add(new IdentityRole<Guid>
            {
                Id = Guid.Parse("2C991241-E7F6-4E39-B5DB-90DDE1DC8785"),
                Name = "Employee",
                NormalizedName = "Employee",
                ConcurrencyStamp = "f0bdbfbb-6ea7-4f21-85b3-18c06972e002"
            });

            context.UserRoles.Add(new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                RoleId = Guid.Parse("2C991241-E7F6-4E39-B5DB-90DDE1DC8785")
            });

            await context.SaveChangesAsync();

            var res1 = await context.UserRoles.ToListAsync();

            EmployeeViewModel model = new EmployeeViewModel()
            {
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                Email = "test@gmail.com"
            };

            await service.RemoveEmployeeAsync(model);

            var res2 = await context.UserRoles.ToListAsync();

            Assert.NotEqual(res1.Count(), res2.Count());

            context.Dispose();
        }

    }
}
