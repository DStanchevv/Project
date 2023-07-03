using SportWave.ViewModels.MenAndWomenViewModels;

namespace SportWave.Services.Contracts
{
    public interface IMenAndWomanService
    {
        Task<IEnumerable<MenAndWomenViewModel>> GetProductsAsync(int gender);
    }
}
