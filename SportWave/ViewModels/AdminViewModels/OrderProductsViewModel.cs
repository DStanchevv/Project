namespace SportWave.ViewModels.AdminViewModels
{
    public class OrderProductsViewModel
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Size { get; set; } = null!;
        public int Quantity { get; set; }
        public string ImgUrl { get; set; } = null!;
    }
}
