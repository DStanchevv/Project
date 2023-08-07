using SportWave.Data.Models;

namespace SportWave.ViewModels.StoreViewModels
{
    public class AllStoresViewModel
    {
        public IEnumerable<StoreViewModel> Stores = new HashSet<StoreViewModel>();
        public StoreViewModel? ClosestStore { get; set; }
    }
}
