using System.ComponentModel.DataAnnotations;
using System.Configuration;
using static SportWave.Common.EntityValidationConstants.PaymentMethod;
using static SportWave.Common.EntityValidationConstants.Address;

namespace SportWave.ViewModels.CheckoutViewModels
{
    public class PayWithCardViewModel
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
        public string Provider { get; set; } = null!;

        [StringLength(CardNumberMaxLength, MinimumLength = CardNumberMinLength, ErrorMessage = "Invalid Account number length.")]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public DateTime ExpiryDate { get; set; }

        [RegexStringValidator("^[0-9]{3,4}$")]
        [StringLength(SecurityNumberMaxLength, MinimumLength = SecurityNumberMinLength, ErrorMessage = "Invalid Security code.")]
        public string SecurityCode { get; set; } = null!;

        public string? Msg { get; set; }
    }
}
