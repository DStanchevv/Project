using SportWave.ViewModels.MenViewModels;

namespace SportWave.Services.Contracts
{
    public interface IMenService
    {
        Task<IEnumerable<MenViewModel>> GetMenProductsAsync();
    }
}
