using Microsoft.EntityFrameworkCore;
using SportWave.Stripe;
using Stripe.Checkout;
using Stripe;
using SportWave.Services.Contracts;
using SportWave.Data;
using Microsoft.Extensions.Options;

namespace SportWave.Services
{
    public class StripeCheckoutService : IStripeCheckoutService
    {
        private readonly SportWaveDbContext dbContext;
        private readonly StripeSettings stripeSettings;
        
        public StripeCheckoutService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.stripeSettings = new StripeSettings();
        }
        public async Task<string> CheckoutSessionAsync(Guid userId)
        {
            var shoppingCart = await dbContext.ShoppingCarts.Where(sc => sc.UserId == userId).FirstOrDefaultAsync();
            var products = await dbContext.ShoppingCartItems.Include(p => p.Product).Where(sc => sc.CartId == shoppingCart.Id).ToListAsync();
            var userPromos = await dbContext.PromosUsers.Where(pu => pu.UserId == userId).FirstOrDefaultAsync();
            var userEmail = await dbContext.Users.Where(u => u.Id == userId).Select(u => u.Email).FirstOrDefaultAsync();

            var currency = "usd";
            var successUrl = $"{this.stripeSettings.UrlsHost}/Checkout/OrderThanks";
            var cancelUrl = $"{this.stripeSettings.UrlsHost}/ShoppingCart/ShoppingCart";

            List<SessionLineItemOptions> lineItems = new List<SessionLineItemOptions>();
            foreach (var product in products)
            {
                decimal price = 0;
                if(userPromos != null)
                {
                    var codeValue = await dbContext.PromoCodes.Where(pc => pc.Id == userPromos.PromoCodeId).Select(pc => pc.Value).FirstOrDefaultAsync();
                    price = product.Product.Price - (product.Product.Price * (codeValue / 100m));
                }
                else
                {
                    price = product.Product.Price;
                }

                SessionLineItemOptions item = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = Convert.ToInt64(price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{product.Product.Name}",
                            Description = $"{product.Product.Description} \n Size: {product.Size} \n {product.Product.Color}"
                        }
                    },
                    Quantity = product.Quantity
                };
                lineItems.Add(item);
            }

            StripeConfiguration.ApiKey = stripeSettings.SecretKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);
            session.CustomerEmail = userEmail;

            return session.Url.ToString();
        }
    }
}
