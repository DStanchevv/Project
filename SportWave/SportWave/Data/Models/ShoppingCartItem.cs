using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class ShoppingCartItem
    {
        [ForeignKey(nameof(ShoppingCart))]
        public int CartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
