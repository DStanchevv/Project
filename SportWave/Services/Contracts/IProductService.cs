using SportWave.ViewModels.MenAndWomenViewModels;
using SportWave.ViewModels.ProductViewModels;
using SportWave.ViewModels.ShoppingCart;

namespace SportWave.Services.Contracts
{
    public interface IProductService
    {
        Task AddToCartAsync(ProductDetailsViewModel product, Guid userId);
        Task AddVariationToProductAsync(GetProductWithQuantityAndVariationsViewModel models, int id);
        Task EditProductAsync(EditProductViewModel model, int id);
        Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdAsync(int id);
        Task<ProductDetailsViewModel> GetProductByIdForCartAsync(int id);
        Task<EditProductViewModel> GetProductByIdForEditAsync(int id);
        Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdForRemoveAsync(int id);
        Task<ProductDetailsViewModel> GetProductDetails(int id);
        Task RemoveProductAndVariationsAsync(GetProductWithQuantityAndVariationsViewModel product);
    }
}
