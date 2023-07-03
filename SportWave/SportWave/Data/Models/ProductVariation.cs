using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class ProductVariation
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(ProductGender))]
        public int GenderId { get; set; }
        public ProductGender ProductGender { get; set; } = null!;

        [ForeignKey(nameof(ProductSize))]
        public int SizeId { get; set; }
        public ProductSize ProductSize { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
