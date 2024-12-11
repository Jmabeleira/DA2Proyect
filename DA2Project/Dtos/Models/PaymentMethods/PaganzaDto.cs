using Domain.Models.PaymentMethods;

namespace Dtos.Models.PaymentMethods
{
    public class PaganzaDto : PaymentMethodDto
    {
        public override string Type => "Paganza";
        public override PaymentMethod FromPaymentMethodDtoToPaymentMethod()
        {
            return new Paganza();
        }
    }
}
