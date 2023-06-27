﻿using Microsoft.AspNetCore.Identity;
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
            base.OnModelCreating(builder);
        }
    }
}