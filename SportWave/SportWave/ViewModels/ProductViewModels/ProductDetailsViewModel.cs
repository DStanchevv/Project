using SportWave.Data.Models;

namespace SportWave.ViewModels.ProductViewModels
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Size { get; set; } = null!;

        public IEnumerable<SizesViewModel> Sizes { get; set; } = new List<SizesViewModel>();
        public IEnumerable<ProductVariationModel> ProductVariations { get; set; } = new HashSet<ProductVariationModel>();
    }
}
