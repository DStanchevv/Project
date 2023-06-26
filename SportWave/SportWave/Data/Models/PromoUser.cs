using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class PromoUser
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public IdentityUser User { get; set; } = null!;

        [ForeignKey(nameof(PromoCode))]
        public int PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; } = null!;
    }
}
