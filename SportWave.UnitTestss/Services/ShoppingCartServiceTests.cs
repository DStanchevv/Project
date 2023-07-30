using SportWave.Data.Models;
using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;
using Microsoft.EntityFrameworkCore;
using SportWave.ViewModels.ShoppingCart;

namespace SportWave.UnitTestss.Services
{
    public class ShoppingCartServiceController
    {
        [Fact]
        public async Task AddQuantityToProductAsyncNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            await service.AddQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);
            
            context.Dispose();
        }

        [Fact]
        public async Task AddQuantityToProductAsyncNoPromoCodeTotalPriceChanges()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            await service.AddQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task AddQuantityToProductAsyncWithPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            await service.AddQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task AddQuantityToProductAsyncWithPromoCodeTotalPriceChanges()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            await service.AddQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task ApplyDiscountAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            await context.SaveChangesAsync();
            AddPromoCodeViewModel model = new AddPromoCodeViewModel()
            {
                Code = "test"
            };


            var quantity1 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            await service.ApplyDiscountAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var quantity2 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task ApplyDiscountAsyncWorksProperlyWhenCodeIsNotValid()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            await context.SaveChangesAsync();
            AddPromoCodeViewModel model = new AddPromoCodeViewModel()
            {
                Code = "test"
            };


            var quantity1 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            await service.ApplyDiscountAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var quantity2 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.Equal(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task ApplyDiscountAsyncWorksProperlyWhenCodeIsNull()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            await context.SaveChangesAsync();
            AddPromoCodeViewModel model = new AddPromoCodeViewModel()
            {
                Code = "test"
            };


            var quantity1 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            await service.ApplyDiscountAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var quantity2 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.Equal(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductsInCartAsyncWhenCartIsNotNullAndThereAreProductsAndThereIsPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();


            var result = await service.GetProductsInCartAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductsInCartAsyncWhenCartIsNotNullAndThereAreNoProductsAndThereIsPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();


            var result = await service.GetProductsInCartAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductsInCartAsyncWhenCartIsNotNullAndThereAreProductsAndThereIsNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            await context.SaveChangesAsync();


            var result = await service.GetProductsInCartAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductsInCartAsyncWhenCartIsNotNullAndThereAreNoProductsAndThereIsNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            await context.SaveChangesAsync();


            var result = await service.GetProductsInCartAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductsInCartAsyncWhenCartIsNull()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            await context.SaveChangesAsync();


            var result = await service.GetProductsInCartAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveDiscountAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();


            await service.RemoveDiscountAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));
            var promoUsers = await context.PromosUsers.ToListAsync();

            Assert.Empty(promoUsers);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveDiscountAsyncChangesTotalPrice()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 27m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            var price1 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();
            await service.RemoveDiscountAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));
            var price2 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(price1, price2);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveDiscountAsyncDoesNotChangesTotalPriceWhenThereIsNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            await context.SaveChangesAsync();

            var price1 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();
            await service.RemoveDiscountAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));
            var price2 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.Equal(price1, price2);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveProductFromCartWorksProperlyWhenThereIsPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();


            await service.RemoveProductFromCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);
            var result = await context.ShoppingCartItems.ToListAsync();

            Assert.Empty(result);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveProductFromCartWorksProperlyWhenThereIsNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            await context.SaveChangesAsync();


            await service.RemoveProductFromCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);
            var result = await context.ShoppingCartItems.ToListAsync();

            Assert.Empty(result);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveProductFromCartChangesTotalPriceWhenThereIsPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = false
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            var total1 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();
            await service.RemoveProductFromCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);
            var total2 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(total1, total2);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveProductFromCartChangesTotalPriceWhenThereIsNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 1
            });

            await context.SaveChangesAsync();

            var total1 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();
            await service.RemoveProductFromCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);
            var total2 = await context.ShoppingCarts.Where(c => c.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(total1, total2);

            context.Dispose();
        }

        [Fact]
        public async Task SubtractQuantityToProductAsyncNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 2
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            await service.SubtractQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task SubtractQuantityToProductAsyncNoPromoCodeTotalPriceChanges()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 2
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            await service.SubtractQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task SubtractQuantityToProductAsyncWithPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 2
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            await service.SubtractQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699") && sci.ProductId == 1).Select(c => c.Quantity).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }

        [Fact]
        public async Task SubtractQuantityToProductAsyncWithPromoCodeTotalPriceChanges()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IShoppingCartService service = new ShoppingCartService(context);

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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 2
            });

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699"),
                ProductId = 1,
                Size = "test",
                Quantity = 2
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("F2EFB470-5DE0-4BE6-A618-6FFFC772B506"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            var quantity1 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            await service.SubtractQuantityToProductAsync(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"), 1);

            var quantity2 = await context.ShoppingCarts.Where(c => c.Id == Guid.Parse("BAE2E128-BEE4-4F51-A1F6-9D975FA89699")).Select(c => c.TotalPrice).FirstOrDefaultAsync();

            Assert.NotEqual(quantity1, quantity2);

            context.Dispose();
        }
    }
}
