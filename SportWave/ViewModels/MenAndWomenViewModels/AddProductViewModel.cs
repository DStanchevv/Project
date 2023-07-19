using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Product;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SportWave.ViewModels.MenAndWomenViewModels
{
    public class AddProductViewModel
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

        [Range(1, int.MaxValue)]
        public int GenderId { get; set; }

        [Range(1, 100, ErrorMessage = "No Category selected.")]
        public int CategoryId { get; set; }

        [Required]
        public string ImgUrl { get; set; } = null!;

        public string? Gender { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
        public IEnumerable<GenderViewModel> Genders { get; set; } = new HashSet<GenderViewModel>();
    }
}
