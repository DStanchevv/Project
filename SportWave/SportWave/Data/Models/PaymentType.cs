using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } = null!;
    }
}