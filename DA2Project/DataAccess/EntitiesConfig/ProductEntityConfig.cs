using Dtos.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public static class ProductEntityConfig
    {
        public static void SetProductEntityConfig(EntityTypeBuilder<ProductDto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder
           .HasOne(p => p.BrandDto)
           .WithOne(b => b.Product)
           .HasForeignKey<BrandDto>(b => b.ProductDtoId);

            entityBuilder
                .HasOne(p => p.ColorDto)
                .WithOne(c => c.Product);

            entityBuilder
                .HasOne(p => p.CategoryDto)
                .WithOne(c => c.Product)
                .HasForeignKey<CategoryDto>(c => c.ProductDtoId);

            entityBuilder.HasMany(p => p.CartProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId);
        }
    }
}
