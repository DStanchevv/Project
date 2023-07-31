using System.ComponentModel.DataAnnotations;

namespace SportWave.ViewModels.ShoppingCart
{
    public class AddPromoCodeViewModel
    {
        [Required]
        public string Code { get; set; } = null!;
    }
}
