using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.UserReviews;

namespace SportWave.ViewModels.ProductViewModels
{
    public class AddAndEditReviewViewModel
    {
        public Guid UserId { get; set; }

        [Range(RatingMinValue, RatingMaxValue)]
        public int Rating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string? Comment { get; set; }

        public int ProductId { get; set; }
    }
}
