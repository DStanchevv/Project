using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public IdentityUser User { get; set; } = null!;

        public DateTime DateOfOrder { get; set; }

        [ForeignKey(nameof(PaymentMethod))]
        public int PaymentMethodId { get; set; }
        public UserPaymentMethod PaymentMethod { get; set; } = null!;

        [ForeignKey(nameof(Address))]
        public int ShippingAddressId { get; set; }
        public Address Address { get; set; } = null!;

        public decimal OrderTotal { get; set; }

        [ForeignKey(nameof(OrderStatus))]
        public string Status { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; } = null!;
    }
}
