using SportWave.ViewModels.ProductViewModels;
using System.ComponentModel.DataAnnotations;
using static SportWave.Common.EntityValidationConstants.Variations;

namespace SportWave.ViewModels.MenAndWomenViewModels
{
    public class AddVariationViewModel
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }

        public int GenderId { get; set; }

        [Range(QuantityMinValue, QuantityMaxValue, ErrorMessage = "Invalid quantity.")]
        public int Quantity { get; set; }

        public IEnumerable<SizesViewModel> Sizes { get; set; } = new HashSet<SizesViewModel>();
    }
}
