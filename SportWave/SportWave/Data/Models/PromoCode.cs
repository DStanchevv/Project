using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.PromoCode;

namespace SportWave.Data.Models
{
    public class PromoCode
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CodeMaxLength)]
        public string Code { get; set; } = null!;

        [Range(1, 100)]
        public int Value { get; set; }

        public bool isValid { get; set; }
    }
}
