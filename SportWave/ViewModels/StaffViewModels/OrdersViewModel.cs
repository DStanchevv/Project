using SportWave.ViewModels.ShoppingCart;

namespace SportWave.ViewModels.AdminViewModels
{
    public class OrdersViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string Coutnry { get; set; } = null!;
        public string Town { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public int StreetNumber { get; set; }
        public string? AdditionalInfo { get; set; }
        public string Status { get; set; } = null!;
        public decimal OrderTotal { get; set; }
    }
}
