using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.AdminViewModels;
using SportWave.ViewModels.MenAndWomenViewModels;
using System.Globalization;
using static SportWave.Common.RolesConstants;

namespace SportWave.Services
{
    public class StaffService : IStaffService
    {

        private readonly SportWaveDbContext dbContext;
        private readonly UserManager<ApplicationUser> manager;

        public StaffService(SportWaveDbContext dbContext, UserManager<ApplicationUser> manager)
        {
            this.dbContext = dbContext;
            this.manager = manager;
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

        public async Task AddProductAsync(AddProductViewModel model, string imgUrl)
        {
            var category = await dbContext.ProductCategories.Where(pc => pc.Id == model.CategoryId).Select(pc => pc.Category).FirstOrDefaultAsync();
            if (category != "All" && category != null)
            {

                Product product = new Product()
                {
                    Name = model.Name,
                    Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture),
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    Color = model.Color,
                    GenderId = model.GenderId,
                    ImgUrl = imgUrl
                };

                var gender = await dbContext.ProductGenders.Where(g => g.Id == product.GenderId).FirstOrDefaultAsync();
                if (gender != null)
                {
                    model.Gender = gender.Gender;
                }


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

        public async Task AddPromoCodeAsync(AddNewPromoCodeViewModel model)
        {
            PromoCode promoCode = new PromoCode()
            {
                Code = model.Code,
                Value = model.Value,
                isValid = true
            };

            if (!dbContext.PromoCodes.Any(pc => pc.Code == model.Code))
            {
                if (promoCode.Value > 0)
                {
                    await dbContext.PromoCodes.AddAsync(promoCode);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task ClearOrderAsync(Guid id)
        {
            var order = await dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            var productsOrders = await dbContext.ProductsOrders.Where(po => po.OrderId == id).ToListAsync();

            if (order != null && productsOrders != null && order.Status == "Shipped")
            {
                dbContext.ProductsOrders.RemoveRange(productsOrders);
                dbContext.Orders.Remove(order);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<MakeUserEmployeeViewModel> GetAdminAndEmployeeEmailsAsync()
        {
            HashSet<ApplicationUser> adminEmails = new HashSet<ApplicationUser>();
            HashSet<ApplicationUser> employeeEmails = new HashSet<ApplicationUser>();

            foreach (var user in await dbContext.Users.ToListAsync())
            {
                if(await manager.IsInRoleAsync(user, AdminRoleName))
                {
                    adminEmails.Add(user);
                }
                else if (await manager.IsInRoleAsync(user, EmployeeRoleName))
                {
                    employeeEmails.Add(user);
                }
            }

            var model = new MakeUserEmployeeViewModel()
            {
                AdminEmails = adminEmails,
                EmployeeEmails = employeeEmails
            };
            
            return model;
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

        public async Task<IEnumerable<PromoCodesViewModel>> GetPromoCodesAsync()
        {
            var promoCodes = await dbContext.PromoCodes.Select(pc => new PromoCodesViewModel
            {
                CodeId = pc.Id,
                Code = pc.Code,
                Value = pc.Value,
                IsValid = pc.isValid
            }).ToListAsync();

            return promoCodes;
        }

        public async Task<PromoCodesViewModel> GetPromoCodeToChangeStatusAsync(Guid codeId)
        {
            return await dbContext.PromoCodes.Where(pc => pc.Id == codeId).Select(pc => new PromoCodesViewModel
            {
                CodeId = pc.Id
            }).FirstOrDefaultAsync();
        }

        public async Task MakeInvalidAsync(PromoCodesViewModel model)
        {
            var code = await dbContext.PromoCodes.Where(pc => pc.Id == model.CodeId).FirstOrDefaultAsync();

            if (code != null)
            {
                code.isValid = false;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task MakeUserEmployeeAsync(MakeUserEmployeeViewModel model)
        {
            var user = await dbContext.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync();

            if (user != null)
            {
                await manager.AddToRoleAsync(user, EmployeeRoleName);
            }
        }

        public async Task MakeValidAsync(PromoCodesViewModel promoCode)
        {
            var code = await dbContext.PromoCodes.Where(pc => pc.Id == promoCode.CodeId).FirstOrDefaultAsync();
            if (code != null)
            {
                code.isValid = true;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task SendOrderAsync(Guid id)
        {
            var order = await dbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (order != null)
            {
                var status = await dbContext.OrderStatuses.Where(s => s.Status == "On the way").FirstOrDefaultAsync();
                if (status != null)
                {
                    order.Status = status.Status;
                }
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<EmployeeViewModel> GetEmployeeByIdAsync(Guid userId)
        {
            return await dbContext.Users.Where(u => u.Id == userId).Select(u => new EmployeeViewModel
            {
                UserId = u.Id,
                Email = u.Email
            }).FirstOrDefaultAsync();
        }

        public async Task RemoveEmployeeAsync(EmployeeViewModel employee)
        {
            var role = await dbContext.Roles.Where(r => r.Name == EmployeeRoleName).FirstOrDefaultAsync();
            var employeeToRemove = await dbContext.UserRoles.Where(u => u.UserId == employee.UserId && u.RoleId == role.Id).FirstOrDefaultAsync();

            if (employeeToRemove != null)
            {
                dbContext.UserRoles.Remove(employeeToRemove);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
