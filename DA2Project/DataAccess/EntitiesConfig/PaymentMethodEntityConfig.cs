using Dtos.Models;
using Dtos.Models.PaymentMethods;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public class PaymentMethodEntityConfig
    {
        public static void SetPaymentMethodEntityConfig(EntityTypeBuilder<PaymentMethodDto> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);

            entityBuilder.HasDiscriminator<string>("PaymentMethodType")
                .HasValue<CreditCardDto>("CreditCard")
                .HasValue<DebitCardDto>("DebitCard")
                .HasValue<PaganzaDto>("Paganza")
            .HasValue<PaypalDto>("Paypal");

            entityBuilder.HasOne(x => x.Order)
               .WithOne(x => x.PaymentMethod)
               .HasPrincipalKey<OrderDto>(x => x.Id);
        }
    }
}
