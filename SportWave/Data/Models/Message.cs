using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Msg { get; set; } = null!;

        public DateTime Time { get; set; }
    }
}
