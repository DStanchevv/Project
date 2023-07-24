using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Category;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SportWave.ViewModels.MenAndWomenViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Invalid Category length.")]
        public string Name { get; set; } = null!;
    }
}
