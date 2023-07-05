using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class PromoUser
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(PromoCode))]
        public Guid PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; } = null!;
    }
}
