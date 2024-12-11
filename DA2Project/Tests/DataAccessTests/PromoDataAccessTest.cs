using DataAccess.Exceptions;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using Promo20OffDlls.Models;

namespace Tests.DataAccessTests
{
    [TestClass]
    public class PromoDataAccessTest
    {
        IPromoRepository _promoRepository;

        [TestMethod]
        public void TestGetAllPromos()
        {
            Mock<IReflectionManager> mock = new Mock<IReflectionManager>(MockBehavior.Strict);
            mock.Setup(r => r.GetAllPromotions()).Returns(new List<Promotion> { new Promo20Off() });
            _promoRepository = new PromoRepository(mock.Object);

            var result = _promoRepository.GetAllPromos();

            mock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void TestGetAllPromosInvalid()
        {
            Mock<IReflectionManager> mock = new Mock<IReflectionManager>(MockBehavior.Strict);
            mock.Setup(r => r.GetAllPromotions()).Returns(new List<Promotion> { });
            _promoRepository = new PromoRepository(mock.Object);
            Exception exception = null;

            try
            {
                var result = _promoRepository.GetAllPromos();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            mock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.IsTrue(exception.Message.Equals("There are no promos"));

        }

        [TestMethod]
        public void TestGetPromo()
        {
            Promo20Off promo20 = new Promo20Off();
            Mock<IReflectionManager> mock = new Mock<IReflectionManager>(MockBehavior.Strict);
            mock.Setup(r => r.GetAllPromotions()).Returns(new List<Promotion> { promo20 });
            _promoRepository = new PromoRepository(mock.Object);

            var result = _promoRepository.GetPromo(promo20.Id);

            mock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(promo20.Id, result.Id);
        }

        [TestMethod]
        public void TestGetPromoInvalid()
        {
            Promo20Off promo20 = new Promo20Off();
            Mock<IReflectionManager> mock = new Mock<IReflectionManager>(MockBehavior.Strict);
            mock.Setup(r => r.GetAllPromotions()).Returns(new List<Promotion> { promo20 });
            _promoRepository = new PromoRepository(mock.Object);
            Exception exception = null;

            try
            {
                _promoRepository.GetPromo(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.IsTrue(exception.Message.Equals("Promo dosent Exist"));
        }

        [TestMethod]
        public void TestUnhandledDataAccessExceptionThrown()
        {
            Mock<IReflectionManager> mock = new Mock<IReflectionManager>(MockBehavior.Strict);
            mock.Setup(r => r.GetAllPromotions()).Throws(new Exception());
            _promoRepository = new PromoRepository(mock.Object);
            Exception exception = null;

            try
            {
                var result = _promoRepository.GetAllPromos();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            mock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledDataAccessException));
            Assert.IsTrue(exception.Message.Equals("A DataAccess error has ocurred: " + exception.InnerException.Message));
        }
    }
}
