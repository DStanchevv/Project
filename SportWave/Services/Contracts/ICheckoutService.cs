using SportWave.ViewModels.CheckoutViewModels;

namespace SportWave.Services.Contracts
{
    public interface ICheckoutService
    {
        Task CheckoutWithCashAsync(PayInCashViewModel model, Guid UserId);
        Task CheckoutWithCardAsync(PayWithCardViewModel model, Guid UserId);
        Task EmptyShoppingCart(Guid UserId);
    }
}
