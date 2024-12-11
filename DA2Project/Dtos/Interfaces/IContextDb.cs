using Dtos.Models;
using Microsoft.EntityFrameworkCore;

namespace Dtos.Interfaces
{
    public interface IContextDb
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<AddressDto> Addresses { get; set; }
        public DbSet<ProductDto> Products { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<BrandDto> Brands { get; set; }
        public DbSet<ColorDto> Colors { get; set; }
        public DbSet<CategoryDto> Categories { get; set; }
        public DbSet<CartProductDto> CartProducts { get; set; }
        public int SaveChanges();
    }
}
