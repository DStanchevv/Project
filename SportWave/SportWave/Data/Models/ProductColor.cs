using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class ProductColor
    {
        [Key]
        public string Color { get; set; } = null!;
    }
}
