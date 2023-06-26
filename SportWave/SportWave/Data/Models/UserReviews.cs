using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportWave.Common.EntityValidationConstants.UserReviews;

namespace SportWave.Data.Models
{
    public class UserReviews
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public IdentityUser User { get; set; } = null!;

        [Range(0, 10)]
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string? Comment { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
