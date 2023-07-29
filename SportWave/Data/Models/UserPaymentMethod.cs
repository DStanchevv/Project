using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class UserPaymentMethod
    {
        public UserPaymentMethod()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(PaymentType))]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; } = null!;
    }
}
