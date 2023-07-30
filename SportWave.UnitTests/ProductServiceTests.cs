using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;
using SportWave.Data.Models;
using SportWave.ViewModels.ProductViewModels;
using Microsoft.EntityFrameworkCore;
using SportWave.ViewModels.ShoppingCart;

namespace SportWave.UnitTests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task AddReviewAsyncAddsReviewWhenThereIsntOneAlready()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            await context.SaveChangesAsync();

            AddAndEditReviewViewModel model = new AddAndEditReviewViewModel()
            {
                Rating = 10,
                Comment = "Test",
                ProductId = 1
            };

            await service.AddReviewAsync(model, 1, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(context.UserReviews);

            context.Dispose();
        }

        [Fact]
        public async Task DoNotAddReviewAsyncAddsReviewWhenThereIsOneAlready()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.UserReviews.Add(new UserReviews
            {
                Id = 1,
                Comment = "Test",
                Rating = 10,
                ProductId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            AddAndEditReviewViewModel model = new AddAndEditReviewViewModel()
            {
                Rating = 10,
                Comment = "Test",
                ProductId = 1
            };

            var count1 = context.UserReviews.Count();

            await service.AddReviewAsync(model, 1, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var count2 = context.UserReviews.Count();

            Assert.Equal(count2, count1);

            context.Dispose();
        }

        [Fact]
        public async Task AddToCratAsyncWhenThereIsNoCartCreatedAndNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            await context.SaveChangesAsync();

            ProductDetailsViewModel model = new ProductDetailsViewModel()
            {
                Id = 1,
                Category = "test",
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                ImageUrl = "test",
                Name = "Test",
                Price = 30m,
                Quantity = 1,
                Size = "test"
            };

            var count = context.ShoppingCartItems.Count();

            await service.AddToCartAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            count = context.ShoppingCartItems.Count();

            Assert.Equal(1, count);

            context.Dispose();
        }

        [Fact]
        public async Task AddToCratAsyncWhenThereIsNoCartCreatedAndPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.PromoCodes.Add(new PromoCode
            {
                Id = Guid.Parse("4F74B878-DBFE-4249-A532-D0CABE660BAD"),
                Code = "test",
                Value = 10,
                isValid = true
            });

            context.PromosUsers.Add(new PromoUser
            {
                PromoCodeId = Guid.Parse("4F74B878-DBFE-4249-A532-D0CABE660BAD"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            ProductDetailsViewModel model = new ProductDetailsViewModel()
            {
                Id = 1,
                Category = "test",
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                ImageUrl = "test",
                Name = "Test",
                Price = 30m,
                Quantity = 1,
                Size = "test"
            };

            var count = context.ShoppingCartItems.Count();

            await service.AddToCartAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            count = context.ShoppingCartItems.Count();

            Assert.Equal(1, count);

            context.Dispose();
        }

        [Fact]
        public async Task AddToCratAsyncWhenThereIsCartCreatedAndNoPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("C46D05F7-6F05-45B4-B993-3F818601EE24"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 0
            });

            await context.SaveChangesAsync();

            ProductDetailsViewModel model = new ProductDetailsViewModel()
            {
                Id = 1,
                Category = "test",
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                ImageUrl = "test",
                Name = "Test",
                Price = 30m,
                Quantity = 1,
                Size = "test"
            };

            var count = context.ShoppingCartItems.Count();

            await service.AddToCartAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            count = context.ShoppingCartItems.Count();

            Assert.Equal(1, count);

            context.Dispose();
        }

        [Fact]
        public async Task AddToCratAsyncWhenThereIsCartCreatedAndPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("C46D05F7-6F05-45B4-B993-3F818601EE24"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 0
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

            ProductDetailsViewModel model = new ProductDetailsViewModel()
            {
                Id = 1,
                Category = "test",
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                ImageUrl = "test",
                Name = "Test",
                Price = 30m,
                Quantity = 1,
                Size = "test"
            };

            var count = context.ShoppingCartItems.Count();

            await service.AddToCartAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            count = context.ShoppingCartItems.Count();

            Assert.Equal(1, count);

            context.Dispose();
        }

        [Fact]
        public async Task AddToCratAsyncTotalChangesWhenThereIsPromoCode()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.ShoppingCarts.Add(new ShoppingCart
            {
                Id = Guid.Parse("C46D05F7-6F05-45B4-B993-3F818601EE24"),
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                TotalPrice = 0
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

            ProductDetailsViewModel model = new ProductDetailsViewModel()
            {
                Id = 1,
                Category = "test",
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                ImageUrl = "test",
                Name = "Test",
                Price = 30m,
                Quantity = 1,
                Size = "test"
            };

            decimal expected = 30 * 0.9m;

            await service.AddToCartAsync(model, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            var sc = await context.ShoppingCarts.Where(sc => sc.UserId == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).FirstOrDefaultAsync();

            Assert.Equal(expected, sc.TotalPrice);

            context.Dispose();
        }

        [Fact]
        public async Task AddVariationToProductAsyncWhenNeededSizeIsAllAndThereAreNoCurrentVariations()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.ProductSizes.AddRange(
            new ProductSize
            {
                Id = 1,
                Size = "test"
            },
            new ProductSize
            {
                Id = 2,
                Size = "All"
            });

            await context.SaveChangesAsync();

            GetProductWithQuantityAndVariationsViewModel model = new GetProductWithQuantityAndVariationsViewModel()
            {
                Id = 1,
                Quantity = 1,
                SizeId = 2
            };

            await service.AddVariationToProductAsync(model, 1);

            var result = await context.ProductsVariations.Where(pv => pv.ProductId == 1).ToListAsync();

            Assert.NotEmpty(result);
            context.Dispose();
        }

        [Fact]
        public async Task AddVariationToProductAsyncWhenNeededSizeIsNotAllAndThereAreNoCurrentVariations()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.ProductSizes.AddRange(
            new ProductSize
            {
                Id = 1,
                Size = "test"
            },
            new ProductSize
            {
                Id = 2,
                Size = "All"
            });

            await context.SaveChangesAsync();

            GetProductWithQuantityAndVariationsViewModel model = new GetProductWithQuantityAndVariationsViewModel()
            {
                Id = 1,
                Quantity = 1,
                SizeId = 1
            };

            await service.AddVariationToProductAsync(model, 1);

            var result = await context.ProductsVariations.Where(pv => pv.ProductId == 1).ToListAsync();

            Assert.NotEmpty(result);
            context.Dispose();
        }

        [Fact]
        public async Task AddVariationToProductAsyncWhenNeededSizeIsAllAndThereAreCurrentVariations()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.ProductSizes.AddRange(
            new ProductSize
            {
                Id = 1,
                Size = "test"
            },
            new ProductSize
            {
                Id = 2,
                Size = "All"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                Quantity = 1,
                SizeId = 1
            });

            await context.SaveChangesAsync();

            var res1 = await context.ProductsVariations.Where(pv => pv.ProductId == 1).Select(pv => pv.Quantity).FirstOrDefaultAsync();

            GetProductWithQuantityAndVariationsViewModel model = new GetProductWithQuantityAndVariationsViewModel()
            {
                Id = 1,
                Quantity = 1,
                SizeId = 2
            };

            await service.AddVariationToProductAsync(model, 1);

            var res2 = await context.ProductsVariations.Where(pv => pv.ProductId == 1).Select(pv => pv.Quantity).FirstOrDefaultAsync();

            Assert.NotEqual(res1, res2);
            context.Dispose();
        }

        [Fact]
        public async Task AddVariationToProductAsyncWhenNeededSizeIsNotAllAndThereAreCurrentVariations()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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

            context.ProductSizes.AddRange(
            new ProductSize
            {
                Id = 1,
                Size = "test"
            },
            new ProductSize
            {
                Id = 2,
                Size = "All"
            });

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                Quantity = 1,
                SizeId = 1
            });

            await context.SaveChangesAsync();

            var res1 = await context.ProductsVariations.Where(pv => pv.ProductId == 1).Select(pv => pv.Quantity).FirstOrDefaultAsync();

            GetProductWithQuantityAndVariationsViewModel model = new GetProductWithQuantityAndVariationsViewModel()
            {
                Id = 1,
                Quantity = 1,
                SizeId = 1
            };

            await service.AddVariationToProductAsync(model, 1);

            var res2 = await context.ProductsVariations.Where(pv => pv.ProductId == 1).Select(pv => pv.Quantity).FirstOrDefaultAsync();

            Assert.NotEqual(res1, res2);
            context.Dispose();
        }

        [Fact]
        public async Task EditProductAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            EditProductViewModel model = new EditProductViewModel()
            {
                CategoryId = 1,
                Price = "30.99",
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                Name = "Test"
            };

            await service.EditProductAsync(model, 1);

            var priceEdited = await context.Products.Where(p => p.Id == 1).Select(p => p.Price).FirstOrDefaultAsync();

            Assert.Equal(30.99m, priceEdited);

            context.Dispose();
        }

        [Fact]
        public async Task EditProductAsyncDoesNotEditWhenPriceIsNegative()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            EditProductViewModel model = new EditProductViewModel()
            {
                CategoryId = 1,
                Price = "-30",
                Color = "test",
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                Name = "Test"
            };

            await service.EditProductAsync(model, 1);

            var priceEdited = await context.Products.Where(p => p.Id == 1).Select(p => p.Price).FirstOrDefaultAsync();

            Assert.Equal(30, priceEdited);

            context.Dispose();
        }

        [Fact]
        public async Task EditReviewAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

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

            context.UserReviews.Add(new UserReviews
            {
                ProductId = 1,
                Id = 1,
                Comment = "test",
                Rating = 10,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            });

            await context.SaveChangesAsync();

            AddAndEditReviewViewModel model = new AddAndEditReviewViewModel()
            {
                Comment = "test",
                ProductId = 1,
                Rating = 8,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")
            };

            await service.EditReviewAsync(model, 1);

            var ratingEdited = await context.UserReviews.Where(ur => ur.Id == 1).Select(p => p.Rating).FirstOrDefaultAsync();

            Assert.Equal(8, ratingEdited);

            context.Dispose();
        }

        [Fact]
        public async Task GetAvailableQuantityAsyncWorkProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

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

            context.ProductsVariations.Add(new ProductVariation
            {
                ProductId = 1,
                GenderId = 1,
                SizeId = 1,
                Quantity = 1
            });

            await context.SaveChangesAsync();

            CartProductViewModel model = new CartProductViewModel()
            {
                Size = "test",
                Quantity = 1
            };

            var result = await service.GetAvailableQuantityAsync(1, model);
            
            Assert.Equal(1, result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductByIdAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetProductByIdAsync(1);

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductByIdForCartAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetProductByIdForCartAsync(1);

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductByIdForEditAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetProductByIdForEditAsync(1);

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetReviewByIdForEditAsyncNotNullWhenUsserIsCorrect()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            context.UserReviews.Add(new UserReviews
            {
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                ProductId = 1,
                Comment = "test",
                Rating = 10,
                Id = 1
            });

            await context.SaveChangesAsync();

            var result = await service.GetReviewByIdForEditReviewAsync(1, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetReviewByIdForEditAsyncNullWhenUsserIsNotCorrect()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            context.UserReviews.Add(new UserReviews
            {
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD8"),
                ProductId = 1,
                Comment = "test",
                Rating = 10,
                Id = 1
            });

            await context.SaveChangesAsync();

            var result = await service.GetReviewByIdForEditReviewAsync(1, Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"));

            Assert.Null(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductByIdForRemoveAsyncNotNullWhenProductExists()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetProductByIdForRemoveAsync(1);

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductByIdForRemoveAsyncNullWhenProductDoesNotExists()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetProductByIdForRemoveAsync(2);

            Assert.Null(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductByIdForReviewAsyncWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetProductByIdForReviewAsync(1);

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task GetProductDetailsWorksProperly()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
                Description = "test test test test test test test test test test test test test test test test test test test test test test",
                CategoryId = 1,
                Color = "test",
                GenderId = 1,
                ImgUrl = "test"
            });

            await context.SaveChangesAsync();

            var result = await service.GetProductDetails(1);

            Assert.NotNull(result);

            context.Dispose();
        }

        [Fact]
        public async Task RemoveProductAndVariationsAsync()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IProductService service = new ProductService(context);

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
                Price = 30,
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
                Quantity = 1
            });

            context.UserReviews.Add(new UserReviews
            {
                Id = 1,
                ProductId = 1,
                UserId = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                Comment = "test",
                Rating = 10
            });

            await context.SaveChangesAsync();

            GetProductWithQuantityAndVariationsViewModel model = new GetProductWithQuantityAndVariationsViewModel()
            {
                Id = 1,
                SizeId = 1,
                Gender = "test",
                Quantity = 1
            };

            await service.RemoveProductAndVariationsAsync(model);

            var result = await context.Products.ToListAsync();

            Assert.Empty(result);

            context.Dispose();
        }
    }
}
