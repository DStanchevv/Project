using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.ProductCategory;

namespace SportWave.Data.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Category { get; set; } = null!;
    }
}