using SportWave.ViewModels.MenAndWomenViewModels;
using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Product;

namespace SportWave.ViewModels.ProductViewModels
{
    public class EditProductViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Invalid Name length.")]
        public string Name { get; set; } = null!;

        [Required]
        public string Price { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Invalid Description length.")]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength, ErrorMessage = "Invalid Color length.")]
        public string Color { get; set; } = null!;

        [Range(CategoryIdMinValue, CategoryIdMaxValue, ErrorMessage = "No Category selected.")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
