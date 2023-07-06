using SportWave.ViewModels.ProductViewModels;

namespace SportWave.ViewModels.ShoppingCart
{
    public class AllProductsInCartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal Price { get; set; }
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
        public string ImgUrl { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
