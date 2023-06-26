using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportWave.Data.Models
{
    public class UserAddress
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public IdentityUser User { get; set; } = null!;

        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;

        public bool IsDefault { get; set; }
    }
}
