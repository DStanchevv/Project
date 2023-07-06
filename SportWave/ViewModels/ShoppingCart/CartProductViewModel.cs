using System.ComponentModel.DataAnnotations;

namespace SportWave.ViewModels.ShoppingCart
{
    public class CartProductViewModel
    {
        [Range(1, 100)]
        public int Quantity { get; set; }
        public string Size { get; set; } = null!;
    }
}
