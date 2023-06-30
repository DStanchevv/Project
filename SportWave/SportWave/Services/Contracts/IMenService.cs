using SportWave.ViewModels.MenViewModels;

namespace SportWave.Services.Contracts
{
    public interface IMenService
    {
        Task AddProductAsync(AddProductViewModel model);
        Task AddProductVariationAsync(AddVariationViewModel model);
        Task<IEnumerable<MenViewModel>> GetMenProductsAsync();
        Task<AddProductViewModel> GetNewAddedProductAsync();
    }
}
