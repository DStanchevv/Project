using SportWave.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using static SportWave.Common.EntityValidationConstants.Address;

namespace SportWave.ViewModels.CheckoutViewModels
{
    public class PayInCashViewModel
    {
        [Required]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(TownNameMaxLength, MinimumLength = TownNameMinLength)]
        public string Town { get; set; } = null!;

        [Required]
        [StringLength(StreetNameMaxLength, MinimumLength = StreetNameMinLength)]
        public string StreetName { get; set; } = null!;

        public int StreetNumber { get; set; }

        [MaxLength(AdditionalInfoMaxLength)]
        public string? AdditionalInfo { get; set; }
    }
}
