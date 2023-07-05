using System.ComponentModel.DataAnnotations;

namespace SportWave.ViewModels.ProductViewModels
{
    public class GetProductWithQuantityAndVariationsViewModel
    {
        public int Id { get; set; }
        public int SizeId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        public IEnumerable<SizesViewModel> Sizes { get; set; } = new List<SizesViewModel>();
        public IEnumerable<ProductVariationModel> ProductVariations { get; set; } = new HashSet<ProductVariationModel>();
    }
}
