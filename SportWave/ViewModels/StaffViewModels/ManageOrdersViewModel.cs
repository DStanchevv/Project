namespace SportWave.ViewModels.AdminViewModels
{
    public class ManageOrdersViewModel
    {
        public IEnumerable<OrdersViewModel> Orders { get; set; } = new HashSet<OrdersViewModel>();
        public IEnumerable<OrderProductsViewModel> OrderProducts { get; set; } = new HashSet<OrderProductsViewModel>();
        public IEnumerable<OrderStatusViewModel> OrderStatuses { get; set; } = new HashSet<OrderStatusViewModel>();
        public string? Status { get; set; }
    }
}
