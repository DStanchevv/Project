using SportWave.Data.Models;
using SportWave.ViewModels.StoreViewModels;

namespace SportWave.Services.Contracts
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreViewModel>> GetAllStoresAsync();
        Task<AllStoresViewModel> GetStoreByCityOrRegionAsync(string city, string region);
        Task<StoreViewModel> FindClosestOne(double yourlat, double yourLon);
    }
}
