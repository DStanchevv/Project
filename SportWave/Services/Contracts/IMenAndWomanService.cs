using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Services.Contracts
{
    public interface IMenAndWomanService
    {
        Task<AllProductsViewModel> GetFilteredProductsAsync(int gender, AllProductsViewModel model);
        Task<AllProductsViewModel> GetProductsAsync(int gender);
    }
}
