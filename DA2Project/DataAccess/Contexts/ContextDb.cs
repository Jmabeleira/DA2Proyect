using DataAccess.EntitiesConfig;
using Dtos.Interfaces;
using Dtos.Models;
using Dtos.Models.PaymentMethods;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class ContextDb : DbContext, IContextDb
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<AddressDto> Addresses { get; set; }
        public DbSet<ProductDto> Products { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<BrandDto> Brands { get; set; }
        public DbSet<ColorDto> Colors { get; set; }
        public DbSet<CategoryDto> Categories { get; set; }
        public DbSet<CartProductDto> CartProducts { get; set; }
        public DbSet<PaymentMethodDto> PaymentMethods { get; set; }
        public DbSet<CreditCardDto> CreditCards { get; set; }
        public DbSet<DebitCardDto> DebitCards { get; set; }

        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserEntityConfig.SetUserEntityConfig(modelBuilder.Entity<UserDto>());
            AddressEntityConfig.SetAddressEntityConfig(modelBuilder.Entity<AddressDto>());
            ProductEntityConfig.SetProductEntityConfig(modelBuilder.Entity<ProductDto>());
            CartProductEntityConfig.SetCartProductEntityConfig(modelBuilder.Entity<CartProductDto>());
            OrderEntityConfig.SetOrderEntityConfig(modelBuilder.Entity<OrderDto>());
            ColorEntityConfig.SetColorEntityConfig(modelBuilder.Entity<ColorDto>());
            PaymentMethodEntityConfig.SetPaymentMethodEntityConfig(modelBuilder.Entity<PaymentMethodDto>());
            CreditCardEntityConfig.SetCreditCardEntityConfig(modelBuilder.Entity<CreditCardDto>());
            DebitCardEntityConfig.SetDebitCardEntityConfig(modelBuilder.Entity<DebitCardDto>());
        }
    }
}
