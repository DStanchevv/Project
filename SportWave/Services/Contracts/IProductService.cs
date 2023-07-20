using SportWave.ViewModels.MenAndWomenViewModels;
using SportWave.ViewModels.ProductViewModels;
using SportWave.ViewModels.ShoppingCart;

namespace SportWave.Services.Contracts
{
    public interface IProductService
    {
        Task AddReviewAsync(AddAndEditReviewViewModel model, int id, Guid UserId);
        Task AddToCartAsync(ProductDetailsViewModel product, Guid userId);
        Task AddVariationToProductAsync(GetProductWithQuantityAndVariationsViewModel models, int id);
        Task EditProductAsync(EditProductViewModel model, int id);
        Task EditReviewAsync(AddAndEditReviewViewModel model, int id);
        Task<int> GetAvailableQuantityAsync(int id, CartProductViewModel model);
        Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdAsync(int id);
        Task<ProductDetailsViewModel> GetProductByIdForCartAsync(int id);
        Task<EditProductViewModel> GetProductByIdForEditAsync(int id);
        Task<AddAndEditReviewViewModel> GetReviewByIdForEditReviewAsync(int id, Guid userId);
        Task<GetProductWithQuantityAndVariationsViewModel> GetProductByIdForRemoveAsync(int id);
        Task<AddAndEditReviewViewModel> GetProductByIdForReviewAsync(int id);
        Task<ProductDetailsViewModel> GetProductDetails(int id);
        Task RemoveProductAndVariationsAsync(GetProductWithQuantityAndVariationsViewModel product);
    }
}
