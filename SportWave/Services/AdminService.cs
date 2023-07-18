using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;
using System.Globalization;

namespace SportWave.Services
{
    public class AdminService : IAdminService
    {

        private readonly SportWaveDbContext dbContext;

        public AdminService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddCategoryAsync(AddCategoryViewModel model)
        {
            ProductCategory category = new ProductCategory()
            {
                Category = model.Name
            };

            if (!dbContext.ProductCategories.Any(c => c.Category == model.Name))
            {
                await dbContext.ProductCategories.AddAsync(category);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddProductAsync(AddProductViewModel model)
        {
            var category = await dbContext.ProductCategories.Where(pc => pc.Id == model.CategoryId).Select(pc => pc.Category).FirstOrDefaultAsync();
            if (category != "All")
            {
                
                Product product = new Product()
                {
                    Name = model.Name,
                    Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture),
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    Color = model.Color,
                    GenderId = model.GenderId,
                    ImgUrl = model.ImgUrl
                };

                var gender = await dbContext.ProductGenders.Where(g => g.Id == product.GenderId).FirstOrDefaultAsync();
                model.Gender = gender.Gender;

                if (!dbContext.Products.Any(p => p.Name == product.Name && p.Color == product.Color && p.GenderId == product.GenderId))
                {
                    if (product.Price >= 0)
                    {
                        await dbContext.Products.AddAsync(product);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task ClearOrderAsync(Guid id)
        {
            var order = await dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            var productsOrders = await dbContext.ProductsOrders.Where(po => po.OrderId == id).ToListAsync();

            dbContext.ProductsOrders.RemoveRange(productsOrders);
            dbContext.Remove(order);

            await dbContext.SaveChangesAsync();
        }

        public async Task<ManageOrdersViewModel> GetFilteredOrdersAsync(ManageOrdersViewModel model)
        {
            List<OrdersViewModel> orders;
            if (!string.IsNullOrEmpty(model.Status))
            {
                orders = await dbContext.Orders.Include(o => o.Address).Where(o => o.Status == model.Status).Select(o => new OrdersViewModel
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
            }
            else 
            {
                orders = await dbContext.Orders.Include(o => o.Address).Select(o => new OrdersViewModel
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
            }


            var orderProducts = await dbContext.ProductsOrders.Include(p => p.Product).ThenInclude(c => c.Category).Select(po => new OrderProductsViewModel
            {
                OrderId = po.OrderId,
                Name = po.Product.Name,
                Category = po.Product.Category.Category,
                Color = po.Product.Color,
                Size = po.Size,
                Quantity = po.Quantity,
                ImgUrl = po.Product.ImgUrl
            }).ToListAsync();

            var orderStatuses = await dbContext.OrderStatuses.Select(os => new OrderStatusViewModel
            {
                Status = os.Status
            }).ToListAsync();

            var viewModel = new ManageOrdersViewModel()
            {
                Orders = orders,
                OrderProducts = orderProducts,
                OrderStatuses = orderStatuses
            };

            return viewModel;

        }

        public async Task<AddProductViewModel> GetNewAddedProductAsync()
        {
            var categories = await dbContext.ProductCategories.Where(pc => pc.Category != "All").Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Category = c.Category
            }).ToListAsync();

            var genders = await dbContext.ProductGenders.Select(g => new GenderViewModel
            {
                Id = g.Id,
                Gender = g.Gender
            }).ToListAsync();

            var model = new AddProductViewModel
            {
                Categories = categories,
                Genders = genders
            };

            return model;
        }

        public async Task<ManageOrdersViewModel> GetOrdersAsync()
        {
            var orders = await dbContext.Orders.Include(o => o.Address).Select(o => new OrdersViewModel
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

            var orderProducts = await dbContext.ProductsOrders.Include(p => p.Product).ThenInclude(c => c.Category).Select(po => new OrderProductsViewModel
            {
                OrderId = po.OrderId,
                Name = po.Product.Name,
                Category = po.Product.Category.Category,
                Color = po.Product.Color,
                Size = po.Size,
                Quantity = po.Quantity,
                ImgUrl = po.Product.ImgUrl
            }).ToListAsync();

            var orderStatuses = await dbContext.OrderStatuses.Select(os => new OrderStatusViewModel
            {
                Status = os.Status
            }).ToListAsync();

            var model = new ManageOrdersViewModel()
            {
                Orders = orders,
                OrderProducts = orderProducts,
                OrderStatuses = orderStatuses
            };

            return model;
        }

        public async Task SendOrderAsync(Guid id)
        {
            var order = await dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            var status = await dbContext.OrderStatuses.Where(s => s.Status == "On the way").FirstOrDefaultAsync();
            order.Status = status.Status;

            await dbContext.SaveChangesAsync();
        }
    }
}
