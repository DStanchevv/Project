using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class OrderStatus
    {
        [Key]
        public string Status { get; set; } = null!;
    }
}