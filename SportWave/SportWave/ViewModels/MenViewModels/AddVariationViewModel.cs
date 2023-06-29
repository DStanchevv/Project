using SportWave.ViewModels.ProductViewModels;
using System.ComponentModel.DataAnnotations;

namespace SportWave.ViewModels.MenViewModels
{
    public class AddVariationViewModel
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        
        [Range(0, 100)]
        public int Quantity { get; set; }

        public IEnumerable<SizeViewModel> Sizes { get; set; } = new HashSet<SizeViewModel>();
    }
}
