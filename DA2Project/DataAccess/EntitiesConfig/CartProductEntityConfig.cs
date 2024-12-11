using Dtos.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public static class CartProductEntityConfig
    {
        public static void SetCartProductEntityConfig(EntityTypeBuilder<CartProductDto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.HasOne(cp => cp.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(cp => cp.OrderId)
                .IsRequired();
        }
    }
}
