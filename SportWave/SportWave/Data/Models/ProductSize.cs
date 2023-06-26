using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class ProductSize
    {
        [Key]
        public string Size { get; set; } = null!;
    }
}
