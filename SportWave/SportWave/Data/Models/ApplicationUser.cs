using Microsoft.AspNetCore.Identity;

namespace SportWave.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Orders = new HashSet<Order>();
            this.UserReviews = new HashSet<UserReviews>();
        }

        public ICollection<Order> Orders { get; set; }
        public ICollection<UserReviews> UserReviews { get; set; }
    }
}
