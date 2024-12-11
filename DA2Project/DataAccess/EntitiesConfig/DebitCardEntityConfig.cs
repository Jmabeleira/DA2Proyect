using Dtos.Models.PaymentMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public class DebitCardEntityConfig
    {
        public static void SetDebitCardEntityConfig(EntityTypeBuilder<DebitCardDto> entityTypeBuilder)
        {
            entityTypeBuilder
            .Property(p => p.DebitCardType)
            .HasColumnType("int");
        }
    }
}
