﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CheckoutWithCashAsync(PayInCashViewModel model, Guid UserId)
        {
            var shoppingCart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).FirstOrDefaultAsync();
            var shoppingCartItems = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCart.Id).ToListAsync();
            var helper = false;

            if (shoppingCartItems.Count > 0)
            {
                Address address = new Address()
                {
                    Country = model.Country,
                    Town = model.Town,
                    StreetName = model.StreetName,
                    StreetNumber = model.StreetNumber,
                    AdditionalInfo = model.AdditionalInfo
                };

                if (!dbContext.Addresses.Any(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo))
                {
                    await dbContext.Addresses.AddAsync(address);
                    await dbContext.SaveChangesAsync();
                }

                var addressId = await dbContext.Addresses.Where(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo).Select(a => a.Id).FirstOrDefaultAsync();
                UserAddress userAddress = new UserAddress
                {
                    UserId = UserId,
                    AddressId = addressId
                };

                if (!dbContext.UsersAddresses.Any(ua => ua.UserId == UserId && ua.AddressId == addressId))
                {
                    await dbContext.UsersAddresses.AddAsync(userAddress);
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
                    await dbContext.SaveChangesAsync();
                }


                var productsInCart = await dbContext.ShoppingCartItems.Include(sci => sci.Product).ToListAsync();
                var total = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sc => sc.TotalPrice).FirstOrDefaultAsync();

                var paymentMethodId = await dbContext.UsersPaymentMethods.Where(pm => pm.UserId == UserId && pm.PaymentTypeId == typeId).Select(upm => upm.Id).FirstOrDefaultAsync();
                Order order = new Order()
                {
                    UserId = UserId,
                    DateOfOrder = DateTime.Now,
                    PaymentMethodId = paymentMethodId,
                    ShippingAddressId = addressId,
                    OrderTotal = total,
                    Status = "Not sent"
                };

                if (!dbContext.Orders.Any(o => o.Id == order.Id))
                {
                    await dbContext.Orders.AddAsync(order);
                }


                foreach (var product in productsInCart)
                {
                    ProductOrder productOrder = new ProductOrder()
                    {
                        OrderId = order.Id,
                        ProductId = product.ProductId,
                        Size = product.Size,
                        Quantity = product.Quantity
                    };

                    if (!dbContext.ProductsOrders.Any(po => po.OrderId == productOrder.OrderId && po.ProductId == productOrder.ProductId && po.Size == productOrder.Size))
                    {
                        await dbContext.ProductsOrders.AddAsync(productOrder);
                    }

                }
                helper = true;
                await dbContext.SaveChangesAsync();
            }
            return helper;
        }

        public async Task<bool> CheckoutWithCardAsync(PayWithCardViewModel model, Guid UserId)
        {
            var shoppingCart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).FirstOrDefaultAsync();
            var shoppingCartItems = await dbContext.ShoppingCartItems.Where(sci => sci.CartId == shoppingCart.Id).ToListAsync();
            bool helper = false;

            if (shoppingCartItems.Count() > 0)
            {
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
                        await dbContext.SaveChangesAsync();

                        Address address = new Address()
                        {
                            Country = model.Country,
                            Town = model.Town,
                            StreetName = model.StreetName,
                            StreetNumber = model.StreetNumber,
                            AdditionalInfo = model.AdditionalInfo
                        };

                        if (!dbContext.Addresses.Any(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo))
                        {
                            await dbContext.Addresses.AddAsync(address);
                            await dbContext.SaveChangesAsync();

                        }

                        var addressId = await dbContext.Addresses.Where(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo).Select(a => a.Id).FirstOrDefaultAsync();
                        UserAddress userAddress = new UserAddress
                        {
                            UserId = UserId,
                            AddressId = addressId
                        };

                        if (!dbContext.UsersAddresses.Any(ua => ua.UserId == UserId && ua.AddressId == addressId))
                        {
                            await dbContext.UsersAddresses.AddAsync(userAddress);
                        }



                        var productsInCart = await dbContext.ShoppingCartItems.Include(sci => sci.Product).ToListAsync();
                        var total = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).Select(sc => sc.TotalPrice).FirstOrDefaultAsync();


                        var paymentMethodId = await dbContext.UsersPaymentMethods.Where(pm => pm.UserId == UserId && pm.PaymentTypeId == typeId).Select(upm => upm.Id).FirstOrDefaultAsync();
                        Order order = new Order()
                        {
                            UserId = UserId,
                            DateOfOrder = DateTime.Now,
                            PaymentMethodId = paymentMethodId,
                            ShippingAddressId = addressId,
                            OrderTotal = total,
                            Status = "Not sent"
                        };

                        if (!dbContext.Orders.Any(o => o.Id == order.Id))
                        {
                            await dbContext.Orders.AddAsync(order);
                        }

                        foreach (var product in productsInCart)
                        {
                            ProductOrder productOrder = new ProductOrder()
                            {
                                OrderId = order.Id,
                                ProductId = product.ProductId,
                                Size = product.Size,
                                Quantity = product.Quantity
                            };

                            if (!dbContext.ProductsOrders.Any(po => po.OrderId == productOrder.OrderId && po.ProductId == productOrder.ProductId && po.Size == productOrder.Size))
                            {
                                await dbContext.ProductsOrders.AddAsync(productOrder);
                            }

                        }
                        helper = true;
                        await dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    var cardInfo = await dbContext.UsersPaymentMethods.Where(pm => pm.UserId == UserId && pm.PaymentTypeId == typeId && pm.CardNumber == method.CardNumber).FirstOrDefaultAsync();
                    if ((!(method.ExpiryDate < DateTime.Now)) && cardInfo.CardNumber == method.CardNumber && cardInfo.SecurityCode == method.SecurityCode)
                    {
                        Address address = new Address()
                        {
                            Country = model.Country,
                            Town = model.Town,
                            StreetName = model.StreetName,
                            StreetNumber = model.StreetNumber,
                            AdditionalInfo = model.AdditionalInfo
                        };

                        if (!dbContext.Addresses.Any(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo))
                        {
                            await dbContext.Addresses.AddAsync(address);
                            await dbContext.SaveChangesAsync();

                        }

                        var addressId = await dbContext.Addresses.Where(a => a.Country == model.Country && a.Town == model.Town && a.StreetName == model.StreetName && a.StreetNumber == model.StreetNumber && a.AdditionalInfo == model.AdditionalInfo).Select(a => a.Id).FirstOrDefaultAsync();
                        UserAddress userAddress = new UserAddress
                        {
                            UserId = UserId,
                            AddressId = addressId
                        };

                        if (!dbContext.UsersAddresses.Any(ua => ua.UserId == UserId && ua.AddressId == addressId))
                        {
                            await dbContext.UsersAddresses.AddAsync(userAddress);
                        }



                        decimal total = 0;
                        var productsInCart = await dbContext.ShoppingCartItems.Include(sci => sci.Product).ToListAsync();
                        foreach (var product in productsInCart)
                        {
                            var price = product.Product.Price;
                            total += (price * product.Quantity);
                        }


                        var paymentMethodId = await dbContext.UsersPaymentMethods.Where(pm => pm.UserId == UserId && pm.PaymentTypeId == typeId).Select(upm => upm.Id).FirstOrDefaultAsync();
                        Order order = new Order()
                        {
                            UserId = UserId,
                            DateOfOrder = DateTime.Now,
                            PaymentMethodId = paymentMethodId,
                            ShippingAddressId = addressId,
                            OrderTotal = total,
                            Status = "Not sent"
                        };

                        if (!dbContext.Orders.Any(o => o.Id == order.Id))
                        {
                            await dbContext.Orders.AddAsync(order);
                        }

                        foreach (var product in productsInCart)
                        {
                            ProductOrder productOrder = new ProductOrder()
                            {
                                OrderId = order.Id,
                                ProductId = product.ProductId,
                                Size = product.Size
                            };

                            if (!dbContext.ProductsOrders.Any(po => po.OrderId == productOrder.OrderId && po.ProductId == productOrder.ProductId && po.Size == productOrder.Size))
                            {
                                await dbContext.ProductsOrders.AddAsync(productOrder);
                            }

                        }

                        helper = true;
                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            return helper;
        }

        public async Task EmptyShoppingCart(Guid UserId)
        {
            var shoppingCart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == UserId).FirstOrDefaultAsync();
            var products = await dbContext.ShoppingCartItems.Where(sc => sc.CartId == shoppingCart.Id).ToListAsync();

            if (products.Count() > 0)
            {
                foreach (var product in products)
                {
                    var productVariation = await dbContext.ProductsVariations.Where(pv => pv.ProductId == product.ProductId && pv.ProductSize.Size == product.Size).FirstOrDefaultAsync();
                    productVariation.Quantity -= product.Quantity;
                }

                shoppingCart.TotalPrice = 0;
                var promoUser = await dbContext.PromosUsers.Where(pu => pu.UserId == UserId).FirstOrDefaultAsync();
                if (promoUser != null)
                {
                    dbContext.PromosUsers.Remove(promoUser);
                }

                dbContext.ShoppingCartItems.RemoveRange(products);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
