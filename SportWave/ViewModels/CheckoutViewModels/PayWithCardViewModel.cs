using System.ComponentModel.DataAnnotations;
using System.Configuration;
using static SportWave.Common.EntityValidationConstants.PaymentMethod;
using static SportWave.Common.EntityValidationConstants.Address;

namespace SportWave.ViewModels.CheckoutViewModels
{
    public class PayWithCardViewModel
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
        public string Provider { get; set; } = null!;

        [StringLength(CardNumberMaxLength, MinimumLength = CardNumberMinLength)]
        public string AccountNumber { get; set; } = null!;

        [Required]
        public DateTime ExpiryDate { get; set; }

        [RegexStringValidator("^[0-9]{3,4}$")]
        [StringLength(SecurityNumberMaxLength, MinimumLength = SecurityNumberMinLength)]
        public string SecurityCode { get; set; } = null!;

        public string? Msg { get; set; }
    }
}
