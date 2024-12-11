using Dtos.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public static class OrderEntityConfig
    {
        public static void SetOrderEntityConfig(EntityTypeBuilder<OrderDto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.HasOne(o => o.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired();

            entityBuilder.HasMany(x => x.Products)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            entityBuilder.HasOne(x => x.AppliedPromotion)
                .WithOne(x => x.Order)
                .HasForeignKey<AppliedPromotionDto>(x => x.OrderId);
        }
    }
}
