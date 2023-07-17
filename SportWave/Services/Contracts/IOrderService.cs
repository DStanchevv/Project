using SportWave.ViewModels.AdminViewModels;

namespace SportWave.Services.Contracts
{
    public interface IOrderService
    {
        Task<ManageOrdersViewModel> GetOrdersAsync(Guid userId);
        Task MarkedAsShippedAsync(Guid id);
    }
}
