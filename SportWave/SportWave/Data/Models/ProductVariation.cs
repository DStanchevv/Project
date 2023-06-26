using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class ProductVariation
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(ProductColor))]
        public string Color { get; set; } = null!;
        public ProductColor ProductColor { get; set; } = null!;

        [ForeignKey(nameof(ProductGender))]
        public string Gender { get; set; } = null!;
        public ProductGender ProductGender { get; set; } = null!;

        [ForeignKey(nameof(ProductSize))]
        public string Size { get; set; } = null!;
        public ProductSize ProductSize { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
