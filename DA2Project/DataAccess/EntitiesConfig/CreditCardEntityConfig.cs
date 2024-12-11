using Dtos.Models.PaymentMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public class CreditCardEntityConfig
    {
        public static void SetCreditCardEntityConfig(EntityTypeBuilder<CreditCardDto> entityTypeBuilder)
        {
            entityTypeBuilder
            .Property(p => p.CreditCardType)
            .HasColumnType("int");
        }
    }
}
