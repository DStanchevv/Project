using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;
using SportWave.ViewModels.ShoppingCart;

namespace SportWave.Services.Contracts
{
    public interface IAdminService
    {
        Task AddCategoryAsync(AddCategoryViewModel model);
        Task AddProductAsync(AddProductViewModel model);
        Task AddPromoCodeAsync(AddNewPromoCodeViewModel model);
        Task ClearOrderAsync(Guid id);
        Task<ManageOrdersViewModel> GetFilteredOrdersAsync(ManageOrdersViewModel model);
        Task<AddProductViewModel> GetNewAddedProductAsync();
        Task<ManageOrdersViewModel> GetOrdersAsync();
        Task<IEnumerable<PromoCodesViewModel>> GetPromoCodesAsync();
        Task<PromoCodesViewModel> GetPromoCodeToChangeStatusAsync(Guid codeId);
        Task MakeInvalidAsync(PromoCodesViewModel model);
        Task MakeValidAsync(PromoCodesViewModel promoCode);
        Task SendOrderAsync(Guid id);
    }
}
