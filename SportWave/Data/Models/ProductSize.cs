using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Size { get; set; } = null!;
    }
}
