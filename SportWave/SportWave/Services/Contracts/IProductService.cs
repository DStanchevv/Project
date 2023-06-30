using SportWave.ViewModels.MenViewModels;
using SportWave.ViewModels.ProductViewModels;

namespace SportWave.Services.Contracts
{
    public interface IProductService
    {
        Task AddVariationToProductAsync(GetProductWithQuantityAndVariationsViewModel models, int id);
        Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdAsync(int id);
        Task<ProductDetailsViewModel> GetProductDetails(int id);
    }
}
