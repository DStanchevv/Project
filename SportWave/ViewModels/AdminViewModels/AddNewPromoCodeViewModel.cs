using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.PromoCode;

namespace SportWave.ViewModels.AdminViewModels
{
    public class AddNewPromoCodeViewModel
    {
        [StringLength(CodeMaxLength, MinimumLength = CodeMinLength, ErrorMessage = "Invalid code name length.")]
        public string Code { get; set; } = null!;
        
        [Range(CodeMinValue, CodeMaxValue, ErrorMessage = "Invalid code value.")]
        public int Value { get; set; }
    }
}
