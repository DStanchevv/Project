namespace SportWave.ViewModels.AdminViewModels
{
    public class ManageOrdersViewModel
    {
        public IEnumerable<OrdersViewModel> Orders { get; set; } = new HashSet<OrdersViewModel>();
        public IEnumerable<OrderProductsViewModel> OrderProducts { get; set; } = new HashSet<OrderProductsViewModel>();
    }
}
