using SportWave.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using static SportWave.Common.EntityValidationConstants.Address;

namespace SportWave.ViewModels.CheckoutViewModels
{
    public class PayInCashViewModel
    {
        [Required]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength, ErrorMessage = "Invalid Country name length.")]
        public string Country { get; set; } = null!;

        [Required]
        [StringLength(TownNameMaxLength, MinimumLength = TownNameMinLength, ErrorMessage = "Invalid Town name length.")]
        public string Town { get; set; } = null!;

        [Required]
        [StringLength(StreetNameMaxLength, MinimumLength = StreetNameMinLength, ErrorMessage = "Invalid Street name length.")]
        public string StreetName { get; set; } = null!;

        public int StreetNumber { get; set; }

        [MaxLength(AdditionalInfoMaxLength, ErrorMessage = "Invalid Info length.")]
        public string? AdditionalInfo { get; set; }

        public string? Msg { get; set; }
    }
}
