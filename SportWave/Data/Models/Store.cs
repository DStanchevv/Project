using System.ComponentModel.DataAnnotations;

namespace SportWave.Data.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; } = null!;

        public string Region { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Location { get; set; } = null!;
    }
}
