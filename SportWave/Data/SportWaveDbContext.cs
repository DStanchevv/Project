using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportWave.Data.Models;

namespace SportWave.Data
{
    public class SportWaveDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SportWaveDbContext(DbContextOptions<SportWaveDbContext> options)
            : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<UserAddress> UsersAddresses { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<ProductSize> ProductSizes { get; set; } = null!;
        public DbSet<ProductGender> ProductGenders { get; set; } = null!;
        public DbSet<ProductVariation> ProductsVariations { get; set; } = null!;
        public DbSet<UserReviews> UserReviews { get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<ProductOrder> ProductsOrders { get; set; } = null!;
        public DbSet<PromoCode> PromoCodes { get; set; } = null!;
        public DbSet<PromoUser> PromosUsers { get; set; } = null!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public DbSet<UserPaymentMethod> UsersPaymentMethods { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAddress>().HasKey(x => new { x.UserId, x.AddressId });
            builder.Entity<ProductVariation>().HasKey(x => new { x.ProductId, x.SizeId, x.GenderId});
            builder.Entity<ShoppingCartItem>().HasKey(x => new { x.CartId, x.ProductId, x.Size });
            builder.Entity<ProductOrder>().HasKey(x => new { x.ProductId, x.OrderId, x.Size });
            builder.Entity<PromoUser>().HasKey(x => new { x.UserId, x.PromoCodeId });
            builder.Entity<Order>().Property(x => x.OrderTotal).HasPrecision(18, 2);
            builder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);

            builder.Entity<PaymentType>().HasData(this.GeneratePaymentTypes());
            builder.Entity<OrderStatus>().HasData(this.GenerateOrderStatuses());
            builder.Entity<ProductSize>().HasData(this.GenerateProductSizes());
            builder.Entity<ProductGender>().HasData(this.GenerateProductGenders());
            builder.Entity<ProductCategory>().HasData(this.GenerateProductCategories());

            base.OnModelCreating(builder);
        }

        private PaymentType[] GeneratePaymentTypes()
        {
            ICollection<PaymentType> paymentTypes = new HashSet<PaymentType>();

            PaymentType paymentType;

            paymentType = new PaymentType()
            {
                Id = 1,
                Type = "Stripe"
            };
            paymentTypes.Add(paymentType);

            return paymentTypes.ToArray();
        }
        private OrderStatus[] GenerateOrderStatuses()
        {
            ICollection<OrderStatus> orderStatuses = new HashSet<OrderStatus>();

            OrderStatus orderStatus;

            orderStatus = new OrderStatus()
            {
                Status = "Not sent"
            };
            orderStatuses.Add(orderStatus);

            orderStatus = new OrderStatus()
            {
                Status = "On the way"
            };
            orderStatuses.Add(orderStatus);

            orderStatus = new OrderStatus()
            {
                Status = "Shipped"
            };
            orderStatuses.Add(orderStatus);

            return orderStatuses.ToArray();
        }
        private ProductSize[] GenerateProductSizes()
        {
            ICollection<ProductSize> productSizes = new List<ProductSize>();

            ProductSize productSize;

            productSize = new ProductSize()
            {
                Id = 1,
                Size = "XS"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Id = 2,
                Size = "S"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Id = 3,
                Size = "M"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Id = 4,
                Size = "L"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Id = 5,
                Size = "XL"
            };
            productSizes.Add(productSize);

            return productSizes.ToArray();
        }
        private ProductGender[] GenerateProductGenders()
        {
            ICollection<ProductGender> productGenders = new HashSet<ProductGender>();

            ProductGender productGender;

            productGender = new ProductGender()
            {
                Id = 1,
                Gender = "Male"
            };
            productGenders.Add(productGender);

            productGender = new ProductGender()
            {
                Id = 2,
                Gender = "Female"
            };
            productGenders.Add(productGender);

            return productGenders.ToArray();
        }
        private ProductCategory[] GenerateProductCategories()
        {
            ICollection<ProductCategory> productCategories = new HashSet<ProductCategory>();

            ProductCategory productCategory;

            productCategory = new ProductCategory()
            {
                Id = 1,
                Category = "T-Shirts"
            };
            productCategories.Add(productCategory);

            productCategory = new ProductCategory()
            {
                Id = 2,
                Category = "Hoodies"
            };
            productCategories.Add(productCategory);

            productCategory = new ProductCategory()
            {
                Id = 3,
                Category = "Shorts"
            };
            productCategories.Add(productCategory);

            return productCategories.ToArray();
        }
    }
}
