using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Address;

namespace SportWave.Data.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(TownNameMaxLength)]
        public string Town { get; set; } = null!;

        [Required]
        [MaxLength(StreetNameMaxLength)]
        public string StreetName { get; set; } = null!;

        public int StreetNumber { get; set; }

        [MaxLength(AdditionalInfoMaxLength)]
        public string? AdditionalInfo { get; set; }


    }
}
