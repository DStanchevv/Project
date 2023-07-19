using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Variations;

namespace SportWave.ViewModels.ShoppingCart
{
    public class CartProductViewModel
    {
        [Range(QuantityMinValue, QuantityMaxValue, ErrorMessage = "Invalid quantity.")]
        public int Quantity { get; set; }
        public string Size { get; set; } = null!;
    }
}
