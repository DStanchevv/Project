using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Services.Contracts
{
    public interface IAdminService
    {
        Task AddCategoryAsync(AddCategoryViewModel model);
        Task AddProductAsync(AddProductViewModel model);
        Task ClearOrderAsync(Guid id);
        Task<ManageOrdersViewModel> GetFilteredOrdersAsync(ManageOrdersViewModel model);
        Task<AddProductViewModel> GetNewAddedProductAsync();
        Task<ManageOrdersViewModel> GetOrdersAsync();
        Task SendOrderAsync(Guid id);
    }
}
