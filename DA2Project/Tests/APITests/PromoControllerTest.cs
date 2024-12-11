using Domain.Interfaces;
using Domain.Models;
using ECommerceAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Promo3X2Dlls.Models;

namespace Tests.APITests
{

    [TestClass]
    public class PromoControllerTest
    {
        private PromosController _promoController;
        [TestMethod]
        public void GetAllTest()
        {
            Promo3X2 promo3X2 = new Promo3X2();
            Mock<IPromoService> promoLogicMock = new Mock<IPromoService>(MockBehavior.Strict);
            promoLogicMock.Setup(logic => logic.GetAllPromos()).Returns(new List<Promotion> { promo3X2 });
            _promoController = new PromosController(promoLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<Promotion> { promo3X2 });
            OkObjectResult result = _promoController.GetAllPromos() as OkObjectResult;
            List<Promotion> objectResult = result.Value as List<Promotion>;

            promoLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode)
                                                     && objectResult.Count.Equals(1));
        }

        [TestMethod]
        public void GetPromoTest()
        {
            Promo3X2 promo3X2 = new Promo3X2();
            Mock<IPromoService> promoLogicMock = new Mock<IPromoService>(MockBehavior.Strict);
            promoLogicMock.Setup(logic => logic.GetPromo(promo3X2.Id)).Returns(promo3X2);
            _promoController = new PromosController(promoLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(promo3X2);
            OkObjectResult result = _promoController.GetPromo(promo3X2.Id) as OkObjectResult;
            Promotion objectResult = result.Value as Promotion;

            promoLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode)
                                                    && objectResult.Equals(promo3X2));
        }
    }
}
