using SportWave.ViewModels.CheckoutViewModels;

namespace SportWave.Services.Contracts
{
    public interface ICheckoutService
    {
        Task<bool> PlaceOrderAsync(PlaceOrderViewModel model, Guid UserId);
        Task EmptyShoppingCart(Guid UserId);
    }
}
