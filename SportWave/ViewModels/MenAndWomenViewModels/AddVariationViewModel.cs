using SportWave.ViewModels.ProductViewModels;
using System.ComponentModel.DataAnnotations;

namespace SportWave.ViewModels.MenAndWomenViewModels
{
    public class AddVariationViewModel
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }

        public int GenderId { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        public IEnumerable<SizesViewModel> Sizes { get; set; } = new HashSet<SizesViewModel>();
    }
}
