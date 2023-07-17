using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.ViewModels.AdminViewModels;

namespace SportWave.Services
{
    public class OrderService : IOrderService
    {
        private readonly SportWaveDbContext dbContext;

        public OrderService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ManageOrdersViewModel> GetOrdersAsync(Guid userId)
        {
            var orders = await dbContext.Orders.Where(o => o.UserId == userId).Include(o => o.Address).Select(o => new OrdersViewModel
            {
                Id = o.Id,
                DateOfOrder = o.DateOfOrder,
                Town = o.Address.Town,
                Coutnry = o.Address.Country,
                StreetName = o.Address.StreetName,
                StreetNumber = o.Address.StreetNumber,
                AdditionalInfo = o.Address.AdditionalInfo,
                Status = o.Status,
                OrderTotal = o.OrderTotal,
            }).ToListAsync();

            var orderProducts = await dbContext.ProductsOrders.Where(po => po.Order.UserId == userId).Include(p => p.Product).ThenInclude(c => c.Category).Select(po => new OrderProductsViewModel
            {
                OrderId = po.OrderId,
                Name = po.Product.Name,
                Category = po.Product.Category.Category,
                Color = po.Product.Color,
                Size = po.Size,
                Quantity = po.Quantity,
                ImgUrl = po.Product.ImgUrl
            }).ToListAsync();

            var model = new ManageOrdersViewModel()
            {
                Orders = orders,
                OrderProducts = orderProducts
            };

            return model;
        }

        public async Task MarkedAsShippedAsync(Guid id)
        {
            var order = await dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            var status = await dbContext.OrderStatuses.Where(s => s.Status == "Shipped").FirstOrDefaultAsync();
            order.Status = status.Status;

            await dbContext.SaveChangesAsync();
        }
    }
}
