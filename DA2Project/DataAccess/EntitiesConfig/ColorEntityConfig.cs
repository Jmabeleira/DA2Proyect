using Dtos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public static class ColorEntityConfig
    {
        public static void SetColorEntityConfig(EntityTypeBuilder<ColorDto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(u => u.Colors)
                                              .HasColumnName("Colors")
                                              .HasConversion(
                                               v => string.Join(',', v),
                                               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            entityBuilder
                .HasOne(e => e.Product)
                 .WithOne(e => e.ColorDto)
                .HasForeignKey<ColorDto>(e => e.ProductDtoId)
                .IsRequired();
        }
    }
}
