using Microsoft.EntityFrameworkCore;
using SportWave.Data;
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

            var product = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCartId && sci.ProductId == id).FirstAsync();

            var productVariationQuantity = await dbContext.ProductsVariations.Where(pv => pv.ProductId == id && pv.ProductSize.Size == product.Size).Select(pv => pv.Quantity).FirstOrDefaultAsync();

            if (product.Quantity < productVariationQuantity)
            {
                product.Quantity++;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllProductsInCartViewModel>> GetProductsInCartAsync(Guid UserId)
        {
            var shoppingCartId = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sp => sp.Id).FirstOrDefaultAsync();

            var productsInCart = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCartId).Select(sci => new AllProductsInCartViewModel
            {
                Id = sci.ProductId,
                Name = sci.Product.Name,
                Category = sci.Product.Category.Category,
                Price = sci.Product.Price,
                Color = sci.Product.Color,
                Size = sci.Size,
                Quantity = sci.Quantity,
                ImgUrl = sci.Product.ImgUrl,
                TotalPrice = sci.Product.Price * sci.Quantity
            }).ToListAsync();

            return productsInCart;
        }

        public async Task RemoveProductFromCart(Guid UserId, int id)
        {
            var shoppingCartId = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sp => sp.Id).FirstOrDefaultAsync();

            var product = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCartId && sci.ProductId == id).FirstAsync();

            dbContext.ShoppingCartItems.Remove(product);

            await dbContext.SaveChangesAsync();
        }

        public async Task SubtractQuantityToProductAsync(Guid UserId, int id)
        {
            var shoppingCartId = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sp => sp.Id).FirstOrDefaultAsync();

            var product = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCartId && sci.ProductId == id).FirstAsync();

            if (product.Quantity > 1)
            {
                product.Quantity--;
            }


            await dbContext.SaveChangesAsync();
        }
    }
}
