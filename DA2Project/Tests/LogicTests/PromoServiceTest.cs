using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Logic.Exceptions;
using Logic.Services;
using Moq;
using Promo20OffDlls.Models;
using PromoFidelityDlls.Models;

namespace Tests.LogicTests
{
    [TestClass]
    public class PromoServiceTest
    {
        PromoService _promoService;

        [TestMethod]
        public void TestGetPromoByIdValid()
        {
            Promo20Off expectedPromo = new Promo20Off();
            Mock<IPromoRepository> mockPromoRepository = new Mock<IPromoRepository>(MockBehavior.Strict);
            mockPromoRepository.Setup(repo => repo.GetPromo(expectedPromo.Id)).Returns(expectedPromo);
            _promoService = new PromoService(mockPromoRepository.Object);

            Promotion actualPromo = _promoService.GetPromo(expectedPromo.Id);

            Assert.AreEqual(expectedPromo, actualPromo);
        }

        [TestMethod]
        public void TestGetPromoByIdInvalid()
        {
            var invalidGuid = Guid.NewGuid();
            Mock<IPromoRepository> mockPromoRepository = new Mock<IPromoRepository>(MockBehavior.Strict);
            mockPromoRepository.Setup(repo => repo.GetPromo(invalidGuid)).Throws(new NoInstanceException(It.IsAny<string>()));
            _promoService = new PromoService(mockPromoRepository.Object);
            Exception exception = null;

            try
            {
                Promotion actualPromo = _promoService.GetPromo(invalidGuid);
            }
            catch (Exception e)
            {
                exception = e;
            }

            mockPromoRepository.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("There is no such Promo"));
        }

        [TestMethod]
        public void TestGetAllPromosValid()
        {
            var expectedPromotions = new List<Promotion>()
           {
               new PromoFidelity(),
               new Promo20Off(),
           };

            Mock<IPromoRepository> mockPromoRepository = new Mock<IPromoRepository>(MockBehavior.Strict);
            mockPromoRepository.Setup(repo => repo.GetAllPromos()).Returns(expectedPromotions);
            _promoService = new PromoService(mockPromoRepository.Object);

            IEnumerable<Promotion> actualPromos = _promoService.GetAllPromos();

            mockPromoRepository.VerifyAll();
            Assert.AreEqual(expectedPromotions, actualPromos);
        }

        [TestMethod]
        public void TestGetAllPromosInvalid()
        {

            Mock<IPromoRepository> promoRepositoryMock = new Mock<IPromoRepository>(MockBehavior.Strict);
            promoRepositoryMock.Setup(p => p.GetAllPromos()).Throws(new NoInstanceException(It.IsAny<string>()));
            _promoService = new PromoService(promoRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _promoService.GetAllPromos();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            promoRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoContentException));
            Assert.IsTrue(exception.Message.Equals("There are no Promos"));
        }

        [TestMethod]
        public void TestApplyPromo()
        {
            var promo = new Promo20Off();
            var expectedPromotions = new List<Promotion>()
           {
               new PromoFidelity(),
               new Promo20Off(),
           };
            Cart cart = new Cart
            {
                User = new User
                {
                    Email = "eren@gmail.com",
                    UserRole = new List<UserRole> { UserRole.Client },
                    Password = "piripitiflauticoMagico",
                    Address = new Address
                    {
                        Country = "Colombia",
                        City = "Bogota",
                        Street = "Calle 123",
                        ZipCode = "123-45",
                        DoorNumber = 201
                    }
                },
                Products = new List<CartProduct>
                {
                    new CartProduct
                    {
                        Product=new Product
                        {
                        Name = "pants",
                        Description = "cool pants",
                        Price = 2000,
                        Brand = "levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "bebidas gaseosas"
                        }
                    },
                    new CartProduct
                    {
                        Product = new Product
                        {
                        Name = "pants2",
                        Description = "cool pants2",
                        Price = 2020,
                        Brand = "levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "bebidas gaseosas"
                        }
                    }
                }
            };

            Mock<IPromoRepository> promoRepositoryMock = new Mock<IPromoRepository>(MockBehavior.Strict);
            promoRepositoryMock.Setup(p => p.GetAllPromos()).Returns(expectedPromotions);
            _promoService = new PromoService(promoRepositoryMock.Object);

            Promotion actualPromo = _promoService.ApplyPromo(cart.Products);

            promoRepositoryMock.VerifyAll();

            Assert.IsNotNull(actualPromo);
            Assert.AreEqual(promo, actualPromo);
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetPromoById()
        {
            var invalidGuid = Guid.NewGuid();
            Mock<IPromoRepository> mockPromoRepository = new Mock<IPromoRepository>(MockBehavior.Strict);
            mockPromoRepository.Setup(repo => repo.GetPromo(invalidGuid)).Throws(new Exception());
            _promoService = new PromoService(mockPromoRepository.Object);
            Exception exception = null;

            try
            {
                Promotion actualPromo = _promoService.GetPromo(invalidGuid);
            }
            catch (Exception e)
            {
                exception = e;
            }

            mockPromoRepository.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetAllPromos()
        {

            Mock<IPromoRepository> promoRepositoryMock = new Mock<IPromoRepository>(MockBehavior.Strict);
            promoRepositoryMock.Setup(p => p.GetAllPromos()).Throws(new Exception());
            _promoService = new PromoService(promoRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _promoService.GetAllPromos();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            promoRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtApplyPromo()
        {
            Mock<IPromoRepository> promoRepositoryMock = new Mock<IPromoRepository>(MockBehavior.Strict);
            promoRepositoryMock.Setup(p => p.GetAllPromos()).Throws(new Exception());
            _promoService = new PromoService(promoRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _promoService.ApplyPromo(new List<CartProduct>());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            promoRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }
    }
}
