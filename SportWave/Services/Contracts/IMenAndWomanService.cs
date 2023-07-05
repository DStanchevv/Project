using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Services.Contracts
{
    public interface IMenAndWomanService
    {
        Task<AllProductsViewModel> GetProductsAsync(int gender);
    }
}
