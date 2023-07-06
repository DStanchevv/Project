using SportWave.ViewModels.ShoppingCart;

namespace SportWave.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task AddQuantityToProductAsync(Guid UserId, int id);
        Task SubtractQuantityToProductAsync(Guid UserId, int id);
        Task<IEnumerable<AllProductsInCartViewModel>> GetProductsInCartAsync(Guid UserId);
        Task RemoveProductFromCart(Guid UserId, int id);
    }
}
