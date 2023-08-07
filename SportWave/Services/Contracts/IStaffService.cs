using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;
using SportWave.ViewModels.StaffViewModels;

namespace SportWave.Services.Contracts
{
    public interface IStaffService
    {
        Task AddCategoryAsync(AddCategoryViewModel model);
        Task AddProductAsync(AddProductViewModel model, string imgUrl);
        Task AddPromoCodeAsync(AddNewPromoCodeViewModel model);
        Task ClearOrderAsync(Guid id);
        Task MakeUserEmployeeAsync(MakeUserEmployeeViewModel model);
        Task<ManageOrdersViewModel> GetFilteredOrdersAsync(ManageOrdersViewModel model);
        Task<AddProductViewModel> GetNewAddedProductAsync();
        Task<ManageOrdersViewModel> GetOrdersAsync();
        Task<IEnumerable<PromoCodesViewModel>> GetPromoCodesAsync();
        Task<PromoCodesViewModel> GetPromoCodeToChangeStatusAsync(Guid codeId);
        Task MakeInvalidAsync(PromoCodesViewModel model);
        Task MakeValidAsync(PromoCodesViewModel promoCode);
        Task SendOrderAsync(Guid id);
        Task<MakeUserEmployeeViewModel> GetAdminAndEmployeeEmailsAsync();
        Task<EmployeeViewModel> GetEmployeeByIdAsync(Guid userId);
        Task RemoveEmployeeAsync(EmployeeViewModel employee);
        Task AddStoreAsync(AddStoreViewModel model);
        Task<IEnumerable<AddStoreViewModel>> GetStoresAsync();
        Task RemoveStoreAsync(int id);
    }
}
