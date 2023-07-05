using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class PromoOrder
    {
        [ForeignKey(nameof(PromoCode))]
        public Guid PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; } = null!;

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
