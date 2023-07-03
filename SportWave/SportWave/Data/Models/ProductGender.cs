using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Product;

namespace SportWave.Data.Models
{
    public class ProductGender
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(GenderMaxLength)]
        public string Gender { get; set; } = null!;
    }
}
