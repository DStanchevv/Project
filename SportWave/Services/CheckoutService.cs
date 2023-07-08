using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.CheckoutViewModels;

namespace SportWave.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly SportWaveDbContext dbContext;

        public CheckoutService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CheckoutWithCashAsync(PayInCashViewModel model, Guid UserId)
        {
            Address address = new Address()
            {
                Country = model.Country,
                Town = model.Town,
                StreetName = model.StreetName,
                StreetNumber = model.StreetNumber,
                AdditionalInfo = model.AdditionalInfo
            };

            UserAddress userAddress = new UserAddress
            {
                UserId = UserId,
                AddressId = address.Id
            };

            if (!dbContext.Addresses.Any(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo))
            {
                await dbContext.Addresses.AddAsync(address);

                if (!dbContext.UsersAddresses.Any(ua => ua.UserId == UserId && ua.AddressId == address.Id))
                {
                    await dbContext.UsersAddresses.AddAsync(userAddress);
                }
            }

            var typeId = await dbContext.PaymentTypes.Where(pt => pt.Type == "Cash").Select(pt => pt.Id).FirstOrDefaultAsync();
            UserPaymentMethod method = new UserPaymentMethod()
            {
                UserId = UserId,
                PaymentTypeId = typeId
            };

            if (!dbContext.UsersPaymentMethods.Any(pm => pm.UserId == UserId && pm.PaymentTypeId == typeId))
            {
                await dbContext.UsersPaymentMethods.AddAsync(method);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task CheckoutWithCardAsync(PayWithCardViewModel model, Guid UserId)
        {
            Address address = new Address()
            {
                Country = model.Country,
                Town = model.Town,
                StreetName = model.StreetName,
                StreetNumber = model.StreetNumber,
                AdditionalInfo = model.AdditionalInfo
            };

            UserAddress userAddress = new UserAddress
            {
                UserId = UserId,
                AddressId = address.Id
            };

            if (!dbContext.Addresses.Any(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo))
            {
                await dbContext.Addresses.AddAsync(address);

                if (!dbContext.UsersAddresses.Any(ua => ua.UserId == UserId && ua.AddressId == address.Id))
                {
                    await dbContext.UsersAddresses.AddAsync(userAddress);
                }
            }

            var typeId = await dbContext.PaymentTypes.Where(pt => pt.Type == "Card").Select(pt => pt.Id).FirstOrDefaultAsync();
            UserPaymentMethod method = new UserPaymentMethod()
            {
                UserId = UserId,
                PaymentTypeId = typeId,
                Provider = model.Provider,
                CardNumber = model.AccountNumber,
                ExpiryDate = model.ExpiryDate,
                SecurityCode = model.SecurityCode
            };

            if (!dbContext.UsersPaymentMethods.Any(pm => pm.UserId == UserId && pm.PaymentTypeId == typeId && pm.CardNumber == method.CardNumber))
            {
                if (!(method.ExpiryDate < DateTime.Now))
                {
                    await dbContext.UsersPaymentMethods.AddAsync(method);
                }
            }
        }

        public async Task EmptyShoppingCart(Guid UserId)
        {
            var shoppingCart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).FirstOrDefaultAsync();
            var products = await dbContext.ShoppingCartItems.Where(sc => sc.CartId == shoppingCart.Id).ToListAsync();


            foreach (var product in products)
            {
                var productVariation = await dbContext.ProductsVariations.Where(pv => pv.ProductId == product.ProductId && pv.ProductSize.Size == product.Size).FirstOrDefaultAsync();
                productVariation.Quantity -= product.Quantity;
            }

            dbContext.ShoppingCartItems.RemoveRange(products);
            await dbContext.SaveChangesAsync();
        }
    }
}
