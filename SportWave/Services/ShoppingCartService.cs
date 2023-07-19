using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.ShoppingCart;

namespace SportWave.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly SportWaveDbContext dbContext;

        public ShoppingCartService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddQuantityToProductAsync(Guid UserId, int id)
        {
            var shoppingCartId = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sp => sp.Id).FirstOrDefaultAsync();

            var product = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCartId && sci.ProductId == id).Include(p => p.Product).FirstAsync();

            var productVariationQuantity = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id && pv.ProductSize.Size == product.Size).Select(pv => pv.Quantity).FirstOrDefaultAsync();

            var cart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).FirstOrDefaultAsync();

            var promoUser = await dbContext.PromosUsers.Where(pu => pu.UserId == UserId).FirstOrDefaultAsync();

            if (promoUser == null)
            {
                if (product.Quantity < productVariationQuantity)
                {
                    product.Quantity++;
                    cart.TotalPrice += product.Product.Price;
                }
            }
            else
            {
                if (product.Quantity < productVariationQuantity)
                {
                    product.Quantity++;
                    var code = await dbContext.PromoCodes.Where(pc => pc.Id == promoUser.PromoCodeId).FirstOrDefaultAsync();
                    cart.TotalPrice += (product.Product.Price - product.Product.Price * (code.Value / 100m));
                }
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task ApplyDiscountAsync(AddPromoCodeViewModel model, Guid userId)
        {
            var code = await dbContext.PromoCodes.Where(pc => pc.Code == model.Code).FirstOrDefaultAsync();

            if (code != null && code.isValid)
            {
                if (!dbContext.PromosUsers.Any(pu => pu.UserId == userId))
                {
                    var cart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == userId).FirstOrDefaultAsync();

                    var shoppingCartItems = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == cart.Id).Include(sci => sci.Product).ToListAsync();

                    cart.TotalPrice -= cart.TotalPrice * (code.Value / 100m);

                    var promoUser = new PromoUser()
                    {
                        UserId = userId,
                        PromoCodeId = code.Id
                    };
                    await dbContext.PromosUsers.AddAsync(promoUser);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<ShoppingCartViewModel> GetProductsInCartAsync(Guid UserId)
        {
            var cart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).FirstOrDefaultAsync();

            if (cart != null)
            {
                var promoUser = await dbContext.PromosUsers.Where(pu => pu.UserId == UserId).FirstOrDefaultAsync();

                if (promoUser != null)
                {
                    var code = await dbContext.PromoCodes.Where(pc => pc.Id == promoUser.PromoCodeId).FirstOrDefaultAsync();

                    var productsInCart = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == cart.Id).Select(sci => new AllProductsInCartViewModel
                    {
                        Id = sci.ProductId,
                        Name = sci.Product.Name,
                        Category = sci.Product.Category.Category,
                        Price = sci.Product.Price,
                        Color = sci.Product.Color,
                        Size = sci.Size,
                        Quantity = sci.Quantity,
                        ImgUrl = sci.Product.ImgUrl,
                        TotalPrice = sci.Product.Price * sci.Quantity,
                        TotalPriceWithPromo = (sci.Product.Price - sci.Product.Price * (code.Value / 100m)) * sci.Quantity
                    }).ToListAsync();

                    var cartModel = new ShoppingCartViewModel()
                    {
                        TotalPrice = cart.TotalPrice,
                        ProductsInCart = productsInCart
                    };

                    return cartModel;
                }
                else
                {
                    var productsInCart = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == cart.Id).Select(sci => new AllProductsInCartViewModel
                    {
                        Id = sci.ProductId,
                        Name = sci.Product.Name,
                        Category = sci.Product.Category.Category,
                        Price = sci.Product.Price,
                        Color = sci.Product.Color,
                        Size = sci.Size,
                        Quantity = sci.Quantity,
                        ImgUrl = sci.Product.ImgUrl,
                        TotalPrice = sci.Product.Price * sci.Quantity,
                        TotalPriceWithPromo = 0
                    }).ToListAsync();

                    var cartModel = new ShoppingCartViewModel()
                    {
                        TotalPrice = cart.TotalPrice,
                        ProductsInCart = productsInCart
                    };

                    return cartModel;
                }

            }
            else
            {
                var shoppingCart = new ShoppingCart()
                {
                    UserId = UserId,
                    TotalPrice = 0
                };
                await dbContext.ShoppingCarts.AddAsync(shoppingCart);
                await dbContext.SaveChangesAsync();

                var productsInCart = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCart.Id).Select(sci => new AllProductsInCartViewModel
                {
                    Id = sci.ProductId,
                    Name = sci.Product.Name,
                    Category = sci.Product.Category.Category,
                    Price = sci.Product.Price,
                    Color = sci.Product.Color,
                    Size = sci.Size,
                    Quantity = sci.Quantity,
                    ImgUrl = sci.Product.ImgUrl,
                    TotalPrice = sci.Product.Price * sci.Quantity,
                    TotalPriceWithPromo = 0
                }).ToListAsync();

                var cartModel = new ShoppingCartViewModel()
                {
                    TotalPrice = shoppingCart.TotalPrice,
                    ProductsInCart = productsInCart
                };

                return cartModel;
            }

        }

        public async Task RemoveDiscountAsync(Guid userId)
        {
            var promoUser = await dbContext.PromosUsers.Where(pu => pu.UserId == userId).FirstOrDefaultAsync();
            var shoppingCart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == userId).FirstOrDefaultAsync();
            var shoppingCartItems = await dbContext.ShoppingCartItems.Include(sci => sci.Product).Where(sci => sci.CartId == shoppingCart.Id).ToListAsync();

            if (promoUser != null)
            {
                dbContext.PromosUsers.Remove(promoUser);
                shoppingCart.TotalPrice = 0;
                foreach (var sci in shoppingCartItems)
                {
                    shoppingCart.TotalPrice += sci.Product.Price * sci.Quantity;
                }

            }

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveProductFromCart(Guid UserId, int id)
        {
            var shoppingCartId = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sp => sp.Id).FirstOrDefaultAsync();

            var product = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCartId && sci.ProductId == id).Include(p => p.Product).FirstOrDefaultAsync();

            var cart = await dbContext.ShoppingCarts.Where(sc => sc.Id == shoppingCartId).FirstOrDefaultAsync();

            var promoUser = await dbContext.PromosUsers.Where(pu => pu.UserId == UserId).FirstOrDefaultAsync();

            if (promoUser != null)
            {
                var code = await dbContext.PromoCodes.Where(pc => pc.Id == promoUser.PromoCodeId).FirstOrDefaultAsync();
                cart.TotalPrice -= (product.Product.Price - product.Product.Price * (code.Value / 100m)) * product.Quantity;
            }
            else
            {
                cart.TotalPrice -= product.Product.Price * product.Quantity;
            }


            dbContext.ShoppingCartItems.Remove(product);

            await dbContext.SaveChangesAsync();
        }

        public async Task SubtractQuantityToProductAsync(Guid UserId, int id)
        {
            var shoppingCartId = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sp => sp.Id).FirstOrDefaultAsync();

            var product = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCartId && sci.ProductId == id).Include(p => p.Product).FirstAsync();

            var cart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).FirstOrDefaultAsync();

            var promoUser = await dbContext.PromosUsers.Where(pu => pu.UserId == UserId).FirstOrDefaultAsync();

            if (promoUser == null)
            {
                if (product.Quantity > 1)
                {
                    product.Quantity--;
                    cart.TotalPrice -= product.Product.Price;
                }
            }
            else
            {
                if (product.Quantity > 1)
                {
                    product.Quantity--;
                    var code = await dbContext.PromoCodes.Where(pc => pc.Id == promoUser.PromoCodeId).FirstOrDefaultAsync();
                    cart.TotalPrice -= (product.Product.Price - product.Product.Price * (code.Value / 100m));
                }
            }


            await dbContext.SaveChangesAsync();
        }
    }
}
