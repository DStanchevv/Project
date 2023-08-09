using SportWave.ViewModels.ShoppingCart;

namespace SportWave.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task AddQuantityToProductAsync(Guid userId, int id);
        Task SubtractQuantityToProductAsync(Guid userId, int id);
        Task<ShoppingCartViewModel> GetProductsInCartAsync(Guid userId);
        Task RemoveProductFromCart(Guid userId, int id);
        Task<bool> ApplyDiscountAsync(AddPromoCodeViewModel model, Guid userId);
        Task<bool> RemoveDiscountAsync(Guid guid);
    }
}
