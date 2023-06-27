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
        public DbSet<ProductColor> ProductColors { get; set; } = null!;
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
        public DbSet<PromoOrder> PromosOrders { get; set; } = null!;
        public DbSet<PromoUser> PromosUsers { get; set; } = null!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public DbSet<UserPaymentMethod> UsersPaymentMethods { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAddress>().HasKey(x => new { x.UserId, x.AddressId });
            builder.Entity<ProductVariation>().HasKey(x => new { x.ProductId, x.Color, x.Size, x.Gender });
            builder.Entity<ShoppingCartItem>().HasKey(x => new { x.CartId, x.ProductId });
            builder.Entity<ProductOrder>().HasKey(x => new { x.ProductId, x.OrderId });
            builder.Entity<PromoOrder>().HasKey(x => new { x.PromoCodeId, x.OrderId });
            builder.Entity<PromoUser>().HasKey(x => new { x.UserId, x.PromoCodeId });
            builder.Entity<Order>().Property(x => x.OrderTotal).HasPrecision(18, 2);
            builder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);

            builder.Entity<PaymentType>().HasData(this.GeneratePaymentTypes());
            builder.Entity<OrderStatus>().HasData(this.GenerateOrderStatus());
            builder.Entity<ProductColor>().HasData(this.GenerateProductColor());
            builder.Entity<ProductSize>().HasData(this.GenerateProductSize());
            builder.Entity<ProductGender>().HasData(this.GenerateProductGender());
            builder.Entity<ProductCategory>().HasData(this.GenerateProductCategory());
            builder.Entity<PromoCode>().HasData(this.GeneratePromoCode());
            builder.Entity<Product>().HasData(this.GenerateProduct());
            builder.Entity<ProductVariation>().HasData(this.GenerateProductVariations());

            base.OnModelCreating(builder);
        }

        private PaymentType[] GeneratePaymentTypes()
        {
            ICollection<PaymentType> paymentTypes = new HashSet<PaymentType>();

            PaymentType paymentType;

            paymentType = new PaymentType()
            {
                Id = 1,
                Type = "Card"
            };
            paymentTypes.Add(paymentType);

            paymentType = new PaymentType()
            {
                Id = 2,
                Type = "Cash"
            };
            paymentTypes.Add(paymentType);

            return paymentTypes.ToArray();
        }

        private OrderStatus[] GenerateOrderStatus()
        {
            ICollection<OrderStatus> orderStatuses = new HashSet<OrderStatus>();

            OrderStatus orderStatus;

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

        private ProductColor[] GenerateProductColor()
        {
            ICollection<ProductColor> productColors = new HashSet<ProductColor>();

            ProductColor productColor;

            productColor = new ProductColor()
            {
                Color = "White"
            };
            productColors.Add(productColor);

            productColor = new ProductColor()
            {
                Color = "Black"
            };
            productColors.Add(productColor);

            productColor = new ProductColor()
            {
                Color = "Red"
            };
            productColors.Add(productColor);

            return productColors.ToArray();
        }

        private ProductSize[] GenerateProductSize()
        {
            ICollection<ProductSize> productSizes = new HashSet<ProductSize>();

            ProductSize productSize;

            productSize = new ProductSize()
            {
                Size = "XS"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Size = "S"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Size = "M"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Size = "L"
            };
            productSizes.Add(productSize);

            productSize = new ProductSize()
            {
                Size = "XL"
            };
            productSizes.Add(productSize);

            return productSizes.ToArray();
        }

        private ProductGender[] GenerateProductGender()
        {
            ICollection<ProductGender> productGenders = new HashSet<ProductGender>();

            ProductGender productGender;

            productGender = new ProductGender()
            {
                Gender = "Male"
            };
            productGenders.Add(productGender);

            productGender = new ProductGender()
            {
                Gender = "Female"
            };
            productGenders.Add(productGender);

            return productGenders.ToArray();
        }

        private ProductCategory[] GenerateProductCategory()
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

        private PromoCode[] GeneratePromoCode()
        {
            ICollection<PromoCode> promoCodes = new HashSet<PromoCode>();

            PromoCode promoCode;

            promoCode = new PromoCode()
            {
                Code = "CODE10",
                Value = 10,
                isValid = true
            };
            promoCodes.Add(promoCode);

            promoCode = new PromoCode()
            {
                Code = "CODE20",
                Value = 20,
                isValid = true
            };
            promoCodes.Add(promoCode);

            return promoCodes.ToArray();
        }

        private Product[] GenerateProduct()
        {
            ICollection<Product> products = new HashSet<Product>();

            Product product;

            product = new Product()
            {
                Id = 1,
                Name = "T-Shirt V1",
                Price = 15.99m,
                Description = "A very light, soft and comfortable T-shirt made of 100% cotton.",
                CategoryId = 1,
                ImgUrl = "/img/T-Shirt V1.jpg"
            };
            products.Add(product);

            product = new Product()
            {
                Id = 2,
                Name = "Hoodie V1",
                Price = 20.99m,
                Description = "A very light, soft and comfortable hoodie made of 100% cotton.",
                CategoryId = 2,
                ImgUrl = "/img/Hoodie V1.jpg"
            };
            products.Add(product);

            product = new Product()
            {
                Id = 3,
                Name = "Shorts V1",
                Price = 20.99m,
                Description = "A very light, soft and comfortable Shorts made of 100% cotton.",
                CategoryId = 3,
                ImgUrl = "/img/Shorts V1.jpg"
            };
            products.Add(product);

            return products.ToArray();
        }

        private ProductVariation[] GenerateProductVariations()
        {
            ICollection<ProductVariation> productsVariations = new HashSet<ProductVariation>();

            ProductVariation productVariation;

            productVariation = new ProductVariation()
            {
                ProductId = 1,
                Color = "White",
                Gender = "Male",
                Size = "S",
                Quantity = 10
            };
            productsVariations.Add(productVariation);

            productVariation = new ProductVariation()
            {
                ProductId = 1,
                Color = "White",
                Gender = "Male",
                Size = "M",
                Quantity = 10
            };
            productsVariations.Add(productVariation);

            productVariation = new ProductVariation()
            {
                ProductId = 2,
                Color = "White",
                Gender = "Male",
                Size = "S",
                Quantity = 10
            };
            productsVariations.Add(productVariation);

            productVariation = new ProductVariation()
            {
                ProductId = 2,
                Color = "White",
                Gender = "Male",
                Size = "M",
                Quantity = 10
            };
            productsVariations.Add(productVariation);

            productVariation = new ProductVariation()
            {
                ProductId = 3,
                Color = "Black",
                Gender = "Male",
                Size = "S",
                Quantity = 10
            };
            productsVariations.Add(productVariation);

            productVariation = new ProductVariation()
            {
                ProductId = 3,
                Color = "Black",
                Gender = "Male",
                Size = "M",
                Quantity = 10
            };
            productsVariations.Add(productVariation);

            return productsVariations.ToArray();
        }

    }
}
