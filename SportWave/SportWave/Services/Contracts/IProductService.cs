using SportWave.ViewModels.MenViewModels;
using SportWave.ViewModels.ProductViewModels;

namespace SportWave.Services.Contracts
{
    public interface IProductService
    {
        Task AddVariationToProductAsync(GetProductWithQuantityAndVariationsViewModel models, int id);
        Task EditProductAsync(EditProductViewModel model, int id);
        Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdAsync(int id);
        Task<EditProductViewModel> GetProductByIdForEditAsync(int id);
        Task<ProductDetailsViewModel> GetProductDetails(int id);
    }
}
