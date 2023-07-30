using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;
using SportWave.Data.Models;
using Microsoft.EntityFrameworkCore;
using SportWave.ViewModels.CheckoutViewModels;

namespace SportWave.UnitTestss.Services
{
    public class CheckoutServiceTests
    {
        [Fact]
        public async Task EmptyShoppingCartWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            ICheckoutService service = new CheckoutService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 1
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            await service.EmptyShoppingCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var items = await context.ShoppingCartItems.Where(sci => sci.CartId == Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA")).ToListAsync();

            Assert.Empty(items);

            context.Dispose();
        }

        [Fact]
        public async Task EmptyShoppingCartResetsPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            ICheckoutService service = new CheckoutService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 1
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("EFFFA5EF-0679-4C4E-8155-84708AAA916D"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            context.PromosUsers.Add(new PromoUser
            {
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                PromoCodeId = Guid.Parse("EFFFA5EF-0679-4C4E-8155-84708AAA916D"),
            });

            await context.SaveChangesAsync();

            await service.EmptyShoppingCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var items = await context.PromosUsers.Where(pu => pu.PromoCodeId == Guid.Parse("EFFFA5EF-0679-4C4E-8155-84708AAA916D")).ToListAsync();

            Assert.Empty(items);

            context.Dispose();
        }
        [Fact]
        public async Task EmptyShoppingCartWorksProperlyResetsCartTotalPrice()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            ICheckoutService service = new CheckoutService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 1
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            await service.EmptyShoppingCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var item = await context.ShoppingCarts.Where(sc => sc.Id == Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA")).FirstOrDefaultAsync();

            Assert.Equal(0, item.TotalPrice);

            context.Dispose();
        }

        [Fact]
        public async Task EmptyShoppingCartWorksProperlyResetsProductVariationCount()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            ICheckoutService service = new CheckoutService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 1
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            await context.SaveChangesAsync();

            await service.EmptyShoppingCart(Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var item = await context.ProductsVariations.Where(pv => pv.ProductId == 1 && pv.SizeId == 1).FirstOrDefaultAsync();

            Assert.Equal(0, item.Quantity);

            context.Dispose();
        }

        [Fact]
        public async Task PlaceOrderWorksProperlyWithItemInShoppingCart()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            ICheckoutService service = new CheckoutService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 1
            });

            context.ShoppingCartItems.Add(new ShoppingCartItem
            {
                CartId = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                ProductId = 1,
                Quantity = 1,
                Size = "test"
            });

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "Stripe"
            });

            await context.SaveChangesAsync();

            PlaceOrderViewModel model = new PlaceOrderViewModel()
            {
                Country = "Test",
                StreetName = "Test",
                StreetNumber = 48,
                AdditionalInfo = "",
                Town = "Test",
                Msg = "Test"
            };

            var result = await service.PlaceOrderAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.True(result);

            context.Dispose();
        }

        [Fact]
        public async Task PlaceOrderWorksProperlyWithoutItemInShoppingCart()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            ICheckoutService service = new CheckoutService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("0337714F-84AD-4A16-9F33-06E6CDC00DCA"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 30m
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

            context.ProductSizes.Add(new ProductSize
            {
                Id = 1,
                Size = "test"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 1
            });

            context.PaymentTypes.Add(new PaymentType
            {
                Id = 1,
                Type = "Stripe"
            });

            await context.SaveChangesAsync();

            PlaceOrderViewModel model = new PlaceOrderViewModel()
            {
                Country = "Test",
                StreetName = "Test",
                StreetNumber = 48,
                AdditionalInfo = "",
                Town = "Test",
                Msg = "Test"
            };

            var result = await service.PlaceOrderAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.False(result);

            context.Dispose();
        }
    }
}
