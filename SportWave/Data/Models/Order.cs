using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public DateTime DateOfOrder { get; set; }

        [ForeignKey(nameof(PaymentMethod))]
        public Guid PaymentMethodId { get; set; }
        public UserPaymentMethod PaymentMethod { get; set; } = null!;

        [ForeignKey(nameof(Address))]
        public Guid ShippingAddressId { get; set; }
        public Address Address { get; set; } = null!;

        public decimal OrderTotal { get; set; }

        [ForeignKey(nameof(OrderStatus))]
        public string Status { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; } = null!;
    }
}
