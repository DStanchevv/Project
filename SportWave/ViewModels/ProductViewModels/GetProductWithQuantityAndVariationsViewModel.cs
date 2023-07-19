using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Variations;

namespace SportWave.ViewModels.ProductViewModels
{
    public class GetProductWithQuantityAndVariationsViewModel
    {
        public int Id { get; set; }
        public int SizeId { get; set; }

        [Range(QuantityMinValue, QuantityMaxValue, ErrorMessage = "Invalid quantity.")]
        public int Quantity { get; set; }

        public string? Gender { get; set; }
        public IEnumerable<SizesViewModel> Sizes { get; set; } = new List<SizesViewModel>();
        public IEnumerable<ProductVariationModel> ProductVariations { get; set; } = new HashSet<ProductVariationModel>();
    }
}
