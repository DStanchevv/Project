namespace SportWave.ViewModels.ProductViewModels
{
    public class ProductVariationModel
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int GenderId { get; set; }
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
