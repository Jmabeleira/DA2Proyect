using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.PaymentMethods;
using Domain.WebModels.Out;
using Logic.Exceptions;
using Logic.Services;
using Moq;
using Promo20OffDlls.Models;
using Promo3X2Dlls.Models;
using PromoFidelityDlls.Models;

namespace Tests.LogicTests
{
    [TestClass]
    public class OrderServiceTest
    {
        private IOrderService _orderService;
        private Promotion promotion = new Promo20Off();

        [TestMethod]
        public void TestAddOrder()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            CartProduct product = new CartProduct
            {
                Product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                },
                Quantity = 1,
            };
            Order order = new Order
            {
                Customer = customer,
                Products = new List<CartProduct> { product },
                Date = new DateTime(2023, 9, 9, 12, 0, 0)
            };

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.AddOrder(It.IsAny<Order>())).Returns(order);

            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            Order expectedOrder = order;
            Order orderResult = _orderService.AddOrder(order);

            orderRepositoryMock.VerifyAll();
            Assert.AreEqual(orderResult, expectedOrder);
        }

        [TestMethod]
        public void TestAddOrderInvalid()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            CartProduct product = new CartProduct
            {
                Product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                },
                Quantity = 1,
            };
            Order order = new Order
            {
                Customer = customer,
                Products = new List<CartProduct> { product },
                Date = new DateTime(2023, 9, 9, 12, 0, 0),
                PaymentMethod = new DebitCard { CardType = DebitCardType.Bbva }
            };

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.AddOrder(It.IsAny<Order>())).Throws(new InstanceNotUniqueException(It.IsAny<string>()));

            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            Exception exception = null;

            try
            {
                _orderService.AddOrder(order);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(AlreadyExistsException));
            Assert.IsTrue(exception.Message.Equals("Order already exists"));
        }


        [TestMethod]
        public void TestGetOrderById()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            CartProduct product = new CartProduct
            {
                Product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                },
                Quantity = 1,
            };
            Order order = new Order
            {
                Customer = customer,
                Products = new List<CartProduct> { product },
                Date = new DateTime(2023, 9, 9, 12, 0, 0),
                PaymentMethod = new DebitCard { CardType = DebitCardType.Itau }
            };
            var orderList = new List<Order> { order };

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Returns(orderList);
            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            List<Order> expectedOrder = orderList;
            List<Order> orderResult = _orderService.GetOrderByUserId(order.Customer.Id);

            orderRepositoryMock.VerifyAll();
            Assert.IsTrue(orderResult.SequenceEqual(expectedOrder));
        }

        [TestMethod]
        public void TestGetOrderByUserIdInvalid()
        {
            Guid guid = Guid.NewGuid();
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Throws(new NoInstanceException(It.IsAny<string>()));
            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            Exception exception = null;

            try
            {
                _orderService.GetOrderByUserId(guid);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("Order does not exist"));
        }

        [TestMethod]
        public void TestGetAllOrders()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            CartProduct product = new CartProduct
            {
                Product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                },
                Quantity = 1,
            };
            Order order = new Order
            {
                Customer = customer,
                Products = new List<CartProduct> { product },
                Date = new DateTime(2023, 9, 9, 12, 0, 0),
                PaymentMethod = new CreditCard() { CardType = CreditCardType.MasterCard }
            };

            List<Order> ordersList = new List<Order> { order };
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Returns(ordersList);

            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            List<Order> expectedOrder = ordersList;
            List<Order> orderResult = _orderService.GetAllOrders();

            orderRepositoryMock.VerifyAll();
            Assert.AreEqual(orderResult, expectedOrder);
        }

        [TestMethod]
        public void TestGetAllOrdersInvalid()
        {
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Throws(new NoInstanceException(It.IsAny<string>()));
            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            Exception exception = null;

            try
            {
                _orderService.GetAllOrders();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoContentException));
            Assert.IsTrue(exception.Message.Equals("There are no Orders"));
        }

        [TestMethod]
        public void TestGetAllOrdersFromCustomerOk()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            CartProduct product = new CartProduct
            {
                Product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                },
                Quantity = 1,
            };
            Order order = new Order
            {
                Customer = customer,
                Products = new List<CartProduct> { product },
                Date = new DateTime(2023, 9, 9, 12, 0, 0),
                AppliedPromotion = new AppliedPromotion()
                {
                    Name = promotion.Name,
                    Description = promotion.Description,
                    Condition = promotion.Condition
                },
                PaymentMethod = new Paganza()
            };

            List<Order> ordersList = new List<Order> { order };
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Returns(ordersList);

            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            List<Order> expectedOrder = ordersList;
            List<Order> orderResult = _orderService.GetOrdersByCustomerId(order.Customer.Id);

            orderRepositoryMock.VerifyAll();
            Assert.IsTrue(orderResult.SequenceEqual(expectedOrder));
        }

        [TestMethod]
        public void TestGetOrdersByCustomerIdInvalid()
        {
            Guid guid = Guid.NewGuid();
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Throws(new NoInstanceException("There are no Orders"));
            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);
            Exception exception = null;

            try
            {
                _orderService.GetOrdersByCustomerId(guid);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoContentException));
            Assert.IsTrue(exception.Message.Equals("User has no Orders"));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtAddOrder()
        {
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.AddOrder(It.IsAny<Order>())).Throws(new Exception());

            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            Exception exception = null;

            try
            {
                _orderService.AddOrder(new Order());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetOrderByUserId()
        {
            Guid guid = Guid.NewGuid();
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Throws(new Exception());
            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            Exception exception = null;

            try
            {
                _orderService.GetOrderByUserId(guid);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetAllOrders()
        {
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Throws(new Exception());
            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            Exception exception = null;

            try
            {
                _orderService.GetAllOrders();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetOrdersByCustomerId()
        {
            Guid guid = Guid.NewGuid();
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.GetAllOrders()).Throws(new Exception());
            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);
            Exception exception = null;

            try
            {
                _orderService.GetOrdersByCustomerId(guid);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestPurchaseOk()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            CartProduct product = new CartProduct
            {
                Product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                },
                Quantity = 1,
            };

            Cart cart = new Cart
            {
                User = customer,
                Products = new List<CartProduct> { product },
                Promotion = new Promo20Off()
            };

            var order = new Order
            {
                Customer = cart.User,
                Products = cart.Products,
                AppliedPromotion = new AppliedPromotion()
                {
                    Name = cart.Promotion.Name,
                    Description = cart.Promotion.Description,
                    Condition = cart.Promotion.Condition
                },
                PaymentMethod = new CreditCard() { CardType = CreditCardType.Visa }
            };

            var cartOut = new CartOut(cart);

            var cartToOrder = new Order
            {
                Id = cartOut.Id,
                Customer = cart.User,
                Products = cartOut.Products,
                AppliedPromotion = new AppliedPromotion
                {
                    Id = cartOut.Promotion.Id,
                    Condition = cartOut.Promotion.Condition,
                    Description = cartOut.Promotion.Description,
                    Name = cartOut.Promotion.Name,
                },
                TotalPrice = cartOut.TotalPrice
            };

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            Mock<IPurchaseService> purchaseServiceMock = new Mock<IPurchaseService>(MockBehavior.Strict);

            orderRepositoryMock.Setup(x => x.AddOrder(It.IsAny<Order>())).Returns(order);
            purchaseServiceMock.Setup(x => x.Purchase(It.IsAny<Order>(), It.IsAny<string>())).Returns(order);

            _orderService = new OrderService(orderRepositoryMock.Object, purchaseServiceMock.Object);

            var result = _orderService.Purchase(cartToOrder, "creditVisa");

            orderRepositoryMock.VerifyAll();

            Assert.AreEqual(result, order);
        }
    }
}
