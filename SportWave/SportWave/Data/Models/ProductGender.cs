using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class ProductGender
    {
        [Key]
        public string Gender { get; set; } = null!;
    }
}
