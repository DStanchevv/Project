using SportWave.ViewModels.ProductViewModels;

namespace SportWave.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductDetailsViewModel> GetProductDetails(int id);
    }
}
