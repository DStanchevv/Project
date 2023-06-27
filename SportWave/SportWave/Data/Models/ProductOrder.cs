using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class ProductOrder
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
