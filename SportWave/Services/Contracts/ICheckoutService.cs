using SportWave.ViewModels.CheckoutViewModels;

namespace SportWave.Services.Contracts
{
    public interface ICheckoutService
    {
        Task<bool> CheckoutWithCashAsync(PayInCashViewModel model, Guid UserId);
        Task EmptyShoppingCart(Guid UserId);
    }
}
