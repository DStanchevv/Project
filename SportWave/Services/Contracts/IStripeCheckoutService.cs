namespace SportWave.Services.Contracts
{
    public interface IStripeCheckoutService
    {
        Task<string> CheckoutSessionAsync(Guid userId);
    }
}
