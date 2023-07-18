using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.MenAndWomenViewModels;
using SportWave.ViewModels.ProductViewModels;
using SportWave.ViewModels.ShoppingCart;
using System.Globalization;

namespace SportWave.Services
{
    public class ProductService : IProductService
    {
        private readonly SportWaveDbContext dbContext;
        public ProductService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddReviewAsync(AddAndEditReviewViewModel model, int id, Guid userId)
        {
            if (!dbContext.UserReviews.Any(ur => ur.UserId == userId && ur.ProductId == id))
            {
                var userReview = new UserReviews()
                {
                    UserId = userId,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    ProductId = id
                };
                await dbContext.UserReviews.AddAsync(userReview);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddToCartAsync(ProductDetailsViewModel product, Guid userId)
        {
            if (!dbContext.ShoppingCarts.Any(p => p.UserId == userId))
            {
                var shoppingCart = new ShoppingCart()
                {
                    UserId = userId,
                    TotalPrice = 0
                };
                await dbContext.ShoppingCarts.AddAsync(shoppingCart);


                var shoppingCartItems = new ShoppingCartItem()
                {
                    CartId = shoppingCart.Id,
                    ProductId = product.Id,
                    Quantity = product.Quantity,
                    Size = product.Size
                };

                if (!dbContext.PromosUsers.Any(pu => pu.UserId == userId))
                {
                    var price = await dbContext.Products.Where(p => p.Id == shoppingCartItems.ProductId).Select(p => p.Price).FirstOrDefaultAsync();
                    shoppingCart.TotalPrice += price * shoppingCartItems.Quantity;
                }
                else
                {
                    var codeId = await dbContext.PromosUsers.Where(pu => pu.UserId == userId).Select(pu => pu.PromoCodeId).FirstOrDefaultAsync();
                    var codeValue = await dbContext.PromoCodes.Where(pc => pc.Id == codeId).Select(pc => pc.Value).FirstOrDefaultAsync();

                    var price = await dbContext.Products.Where(p => p.Id == shoppingCartItems.ProductId).Select(p => p.Price).FirstOrDefaultAsync();
                    price -= price * (codeValue / 100m);
                    shoppingCart.TotalPrice += price * shoppingCartItems.Quantity;
                }

                await dbContext.ShoppingCartItems.AddAsync(shoppingCartItems);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                var shoppingCart = await dbContext.ShoppingCarts.Where(p => p.UserId == userId).FirstOrDefaultAsync();
                if (!dbContext.ShoppingCartItems.Any(p => p.ProductId == product.Id && p.Size == product.Size))
                {
                    var shoppingCartItems = new ShoppingCartItem()
                    {
                        CartId = shoppingCart.Id,
                        ProductId = product.Id,
                        Quantity = product.Quantity,
                        Size = product.Size
                    };

                    if (!dbContext.PromosUsers.Any(pu => pu.UserId == userId))
                    {
                        var price = await dbContext.Products.Where(p => p.Id == shoppingCartItems.ProductId).Select(p => p.Price).FirstOrDefaultAsync();
                        shoppingCart.TotalPrice += price * product.Quantity;
                    }
                    else
                    {
                        var codeId = await dbContext.PromosUsers.Where(pu => pu.UserId == userId).Select(pu => pu.PromoCodeId).FirstOrDefaultAsync();
                        var codeValue = await dbContext.PromoCodes.Where(pc => pc.Id == codeId).Select(pc => pc.Value).FirstOrDefaultAsync();

                        var price = await dbContext.Products.Where(p => p.Id == shoppingCartItems.ProductId).Select(p => p.Price).FirstOrDefaultAsync();
                        price -= price * (codeValue / 100m);
                        shoppingCart.TotalPrice += price * shoppingCartItems.Quantity;
                    }
                    
                    await dbContext.ShoppingCartItems.AddAsync(shoppingCartItems);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task AddVariationToProductAsync(GetProductWithQuantityAndVariationsViewModel model, int id)
        {
            var gender = await dbContext.Products.Where(p => p.Id == id).Select(p => p.GenderId).FirstAsync();
            var sizes = await dbContext.ProductSizes.Select(s => new SizesViewModel
            {
                SizeId = s.Id,
                Size = s.Size
            }).ToListAsync();
            model.Sizes = sizes;

            var variations = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id).Select(pv => new ProductVariationModel
            {
                ProductId = pv.ProductId,
                SizeId = pv.ProductSize.Id,
                GenderId = gender,
                Quantity = pv.Quantity
            }).ToListAsync();
            model.ProductVariations = variations;

            var neededSize = model.Sizes.Where(p => p.SizeId == model.SizeId).Select(p => p.Size).First();
            if (neededSize == "All")
            {
                foreach (var size in model.Sizes)
                {
                    if (size.Size != "All")
                    {
                        if (model.ProductVariations.Any(pv => pv.SizeId == size.SizeId && pv.ProductId == model.Id))
                        {
                            var productVariation = await dbContext.ProductsVariations.Where(pv => pv.SizeId == size.SizeId && pv.ProductId == model.Id).FirstAsync();
                            productVariation.Quantity += model.Quantity;
                        }
                        else
                        {

                            ProductVariation var = new ProductVariation()
                            {
                                ProductId = model.Id,
                                SizeId = size.SizeId,
                                GenderId = gender,
                                Quantity = model.Quantity
                            };

                            await dbContext.ProductsVariations.AddAsync(var);
                        }
                    }
                }
            }
            else
            {
                if (model.ProductVariations.Any(pv => pv.SizeId == model.SizeId && pv.ProductId == model.Id))
                {
                    var productVariation = await dbContext.ProductsVariations.Where(pv => pv.SizeId == model.SizeId && pv.ProductId == model.Id).FirstAsync();
                    productVariation.Quantity += model.Quantity;
                }
                else
                {

                    ProductVariation var = new ProductVariation()
                    {
                        ProductId = model.Id,
                        SizeId = model.SizeId,
                        GenderId = gender,
                        Quantity = model.Quantity
                    };

                    await dbContext.ProductsVariations.AddAsync(var);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task EditProductAsync(EditProductViewModel model, int id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product != null)
            {
                product.Name = model.Name;
                product.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
                product.Description = model.Description;
                product.CategoryId = model.CategoryId;
                product.Color = model.Color;
                product.ImgUrl = model.ImgUrl;
            }

            if (product.Price >= 0)
            {
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task EditReviewAsync(AddAndEditReviewViewModel model, int id)
        {
            var review = await dbContext.UserReviews.FindAsync(id);

            if (review != null)
            {
                review.Rating = model.Rating;
                review.Comment = model.Comment;
                model.ProductId = review.ProductId;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<int> GetAvailableQuantityAsync(int id, CartProductViewModel model)
        {
            var sizeId = await dbContext.ProductSizes.Where(ps => ps.Size == model.Size).Select(ps => ps.Id).FirstOrDefaultAsync();
            var quantity = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id && pv.SizeId == sizeId).Select(pv => pv.Quantity).FirstOrDefaultAsync();

            return quantity;
        }

        public async Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdAsync(int id)
        {
            var sizes = await dbContext.ProductSizes.Select(s => new SizesViewModel
            {
                SizeId = s.Id,
                Size = s.Size
            }).ToListAsync();

            return await dbContext.Products.Where(p => p.Id == id).Select(p => new GetProductWithQuantityAndVariationsViewModel
            {
                Id = p.Id,
                Sizes = sizes
            }).FirstOrDefaultAsync();
        }

        public async Task<ProductDetailsViewModel> GetProductByIdForCartAsync(int id)
        {
            return await dbContext.Products.Where(p => p.Id == id).Select(p => new ProductDetailsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category.Category,
                Color = p.Color,
            }).FirstOrDefaultAsync();
        }

        public async Task<EditProductViewModel> GetProductByIdForEditAsync(int id)
        {
            var categories = await dbContext.ProductCategories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Category = c.Category
            }).ToListAsync();

            return await dbContext.Products.Where(p => p.Id == id).Select(p => new EditProductViewModel
            {
                Name = p.Name,
                Price = p.Price.ToString(),
                Description = p.Description,
                CategoryId = p.CategoryId,
                ImgUrl = p.ImgUrl,
                Color = p.Color,
                Categories = categories
            }).FirstOrDefaultAsync();
        }

        public async Task<AddAndEditReviewViewModel> GetReviewByIdForEditReviewAsync(int id)
        {
            return await dbContext.UserReviews.Where(ur => ur.Id == id).Select(ur => new AddAndEditReviewViewModel
            {
                UserId = ur.UserId,
                Rating = ur.Rating,
                Comment = ur.Comment,
                ProductId = ur.ProductId
            }).FirstOrDefaultAsync();
        }

        public async Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdForRemoveAsync(int id)
        {
            var product = await dbContext.Products.Include(p => p.ProductGender).Where(p => p.Id == id).FirstOrDefaultAsync();
            var gender = product.ProductGender.Gender;

            return await dbContext.Products.Where(p => p.Id == id).Select(p => new GetProductWithQuantityAndVariationsViewModel
            {
                Id = p.Id,
                Gender = gender
            }).FirstOrDefaultAsync();
        }

        public async Task<AddAndEditReviewViewModel> GetProductByIdForReviewAsync(int id)
        {
            return await dbContext.Products.Where(p => p.Id == id).Select(p => new AddAndEditReviewViewModel
            {
                ProductId = id
            }).FirstOrDefaultAsync();
        }

        public async Task<ProductDetailsViewModel> GetProductDetails(int id)
        {
            var sizes = await dbContext.ProductSizes.Select(s => new SizesViewModel
            {
                Size = s.Size
            }).ToListAsync();

            var variations = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id).Select(pv => new ProductVariationModel
            {
                Size = pv.ProductSize.Size,
                Quantity = pv.Quantity
            }).ToListAsync();

            var reviews = await dbContext.UserReviews.Where(ur => ur.ProductId == id).Include(ur => ur.User).ToListAsync();


            return await dbContext.Products.Where(p => p.Id == id).Select(p => new ProductDetailsViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImgUrl,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Color = p.Color,
                Category = p.Category.Category,
                Sizes = sizes,
                ProductVariations = variations,
                Reviews = reviews
            }).FirstOrDefaultAsync();
        }

        public async Task RemoveProductAndVariationsAsync(GetProductWithQuantityAndVariationsViewModel product)
        {
            var productVariations = await dbContext.ProductsVariations.Where(pv => pv.ProductId == product.Id).ToListAsync();

            if (productVariations.Count != 0)
            {
                foreach (var pv in productVariations)
                {
                    dbContext.ProductsVariations.Remove(pv);
                }
            }

            var productReviews = await dbContext.UserReviews.Where(ur => ur.ProductId == product.Id).ToListAsync();
            if(productReviews.Count != 0)
            {
                dbContext.UserReviews.RemoveRange(productReviews);
            }

            var productToBeRemoved = await dbContext.Products.Where(p => p.Id == product.Id).FirstOrDefaultAsync();

            if (productToBeRemoved != null)
            {
                dbContext.Products.Remove(productToBeRemoved);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
