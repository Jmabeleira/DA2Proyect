using Domain.Models.PaymentMethods;
using Dtos.Mappers;
using Dtos.Models.PaymentMethods;

namespace Tests.DataAccessTests
{
    [TestClass]
    public class PaymentVisitorPatternTest
    {
        [TestMethod]
        public void TestFromPaypalToPaypalDto()
        {
            var paymentMethod = new PayPal();

            var paymentMethodDto = PaymentMethodHelper.FromPaymentToPaymentDto(paymentMethod);

            Assert.AreEqual(typeof(PaypalDto), paymentMethodDto.GetType());
        }

        [TestMethod]
        public void TestFromPaganzaToPaganzaDto()
        {
            var paymentMethod = new Paganza();

            var paymentMethodDto = PaymentMethodHelper.FromPaymentToPaymentDto(paymentMethod);

            Assert.AreEqual(typeof(PaganzaDto), paymentMethodDto.GetType());
        }

        [TestMethod]
        public void TestFromPaypalDtoToPaypal()
        {
            var paymentMethodDto = new PaypalDto();

            var paymentMethod = paymentMethodDto.FromPaymentMethodDtoToPaymentMethod();

            Assert.AreEqual(typeof(PayPal), paymentMethod.GetType());
        }

        [TestMethod]
        public void TestFromPaganzaDtoToPaganza()
        {
            var paymentMethodDto = new PaganzaDto();

            var paymentMethod = paymentMethodDto.FromPaymentMethodDtoToPaymentMethod();

            Assert.AreEqual(typeof(Paganza), paymentMethod.GetType());
        }

        [TestMethod]
        public void TestFromCreditCardDtoToCreditCard()
        {
            var paymentMethodDto = new CreditCardDto();

            var paymentMethod = paymentMethodDto.FromPaymentMethodDtoToPaymentMethod();

            Assert.AreEqual(typeof(CreditCard), paymentMethod.GetType());
        }

        [TestMethod]
        public void TestFromCreditCardToCreditCardDto()
        {
            var paymentMethod = new CreditCard();

            var paymentMethodDto = PaymentMethodHelper.FromPaymentToPaymentDto(paymentMethod);

            Assert.AreEqual(typeof(CreditCardDto), paymentMethodDto.GetType());
        }

        [TestMethod]
        public void TestFromDebitCardToDebitCardDto()
        {
            var paymentMethod = new DebitCard();

            var paymentMethodDto = PaymentMethodHelper.FromPaymentToPaymentDto(paymentMethod);

            Assert.AreEqual(typeof(DebitCardDto), paymentMethodDto.GetType());
        }

        [TestMethod]
        public void TestFromDebitCardDtoToDebitCard()
        {
            var paymentMethodDto = new DebitCardDto();

            var paymentMethod = paymentMethodDto.FromPaymentMethodDtoToPaymentMethod();

            Assert.AreEqual(typeof(DebitCard), paymentMethod.GetType());
        }
    }
}
