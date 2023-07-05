using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Address;

namespace SportWave.Data.Models
{
    public class Address
    {
        public Address()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

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
