using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.PaymentMethods;
using Domain.WebModels.In;
using Domain.WebModels.Out;
using ECommerceAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Promo20OffDlls.Models;

namespace Tests.APITests
{
    [TestClass]
    public class OrdersControllerTest
    {
        private OrdersController _ordersController;

        [TestMethod]
        public void TestGetAllOrdersOK()
        {
            User authUser = new User
            {
                Email = "eren2@gmail.com",
                UserRole = new List<UserRole> { UserRole.Admin },
                Password = "piripitifl2auticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
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

            Mock<IOrderService> ordersLogicMock = new Mock<IOrderService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(authUser);
            ordersLogicMock.Setup(logic => logic.GetAllOrders()).Returns(new List<Order> { order });

            _ordersController = new OrdersController(ordersLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<OrderOut> { new OrderOut(order) });
            List<OrderOut> expectedObject = expected.Value as List<OrderOut>;

            OkObjectResult result = _ordersController.GetAllOrders(null) as OkObjectResult;
            List<OrderOut> objectResult = result.Value as List<OrderOut>;

            ordersLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.IsTrue(expectedObject.SequenceEqual(objectResult));
        }

        [TestMethod]
        public void TestGetAllOrdersUnauthorized()
        {
            User unauthorizedUser = new User
            {
                Email = "eren2@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitifl2auticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IOrderService> ordersLogicMock = new Mock<IOrderService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(unauthorizedUser);

            _ordersController = new OrdersController(ordersLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _ordersController.GetAllOrders(null);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void TestGetOrdersByUserIdOk()
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
            var orderList = new List<Order> { order };

            Mock<IOrderService> ordersLogicMock = new Mock<IOrderService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
            ordersLogicMock.Setup(logic => logic.GetOrderByUserId(It.IsAny<Guid>())).Returns(orderList);
            _ordersController = new OrdersController(ordersLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(orderList.Select(x => new OrderOut(x)).ToList());
            List<OrderOut> expectedObject = expected.Value as List<OrderOut>;

            OkObjectResult result = _ordersController.GetOrderByUserId(order.Id) as OkObjectResult;
            List<OrderOut> objectResult = result.Value as List<OrderOut>;

            ordersLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.IsTrue(expectedObject.SequenceEqual(objectResult));
        }

        [TestMethod]
        public void TestGetOrdersByCustomerIdOk()
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

            Mock<IOrderService> ordersLogicMock = new Mock<IOrderService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(order.Customer);
            ordersLogicMock.Setup(logic => logic.GetOrdersByCustomerId(It.IsAny<Guid>())).Returns(new List<Order> { order });
            _ordersController = new OrdersController(ordersLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<OrderOut> { new OrderOut(order) });
            List<OrderOut> expectedObject = expected.Value as List<OrderOut>;

            OkObjectResult result = _ordersController.GetAllOrders(order.Customer.Id) as OkObjectResult;
            List<OrderOut> objectResult = result.Value as List<OrderOut>;

            ordersLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.IsTrue(expectedObject.SequenceEqual(objectResult));
        }

        [TestMethod]
        public void TestPurchase()
        {
            CartIn cartIn = new CartIn
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
                Products = new List<CartProductIn>
                {
                   new CartProductIn
                   {
                       Product = new Product
                       {
                           Name = "Pants",
                           Description = "Cool Pants",
                           Price = 2000,
                           Brand = "Levis",
                           Colors= new List<string>{"red","blue"},
                           Category = "Bebidas gaseosas"
                       },
                       Quantity = 1
                   }
                }
            };
            var cart = cartIn.ToEntity();
            cart.Promotion = new Promo20Off();

            var order = new Order
            {
                Id = cart.Id,
                Customer = cart.User,
                Products = cart.Products,
                PaymentMethod = new DebitCard() { CardType = DebitCardType.Santander }
            };

            var cartOut = new CartOut(cart);

            var cartToOrderIn = new OrderIn
            {
                Id = cartOut.Id,
                User = cart.User,
                Products = cartOut.Products,
                Promotion = new AppliedPromotion
                {
                    Id = cartOut.Promotion.Id,
                    Condition = cartOut.Promotion.Condition,
                    Description = cartOut.Promotion.Description,
                    Name = cartOut.Promotion.Name,
                },
                TotalPrice = cartOut.TotalPrice
            };

            Mock<IOrderService> ordersLogicMock = new Mock<IOrderService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            ordersLogicMock.Setup(logic => logic.Purchase(It.IsAny<Order>(), It.IsAny<string>())).Returns(order);

            _ordersController = new OrdersController(ordersLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new OrderOut(order));
            OrderOut expectedObject = expected.Value as OrderOut;

            OkObjectResult result = _ordersController.Purchase(cartToOrderIn, PaymentKey.DebitCardSantander) as OkObjectResult;
            OrderOut objectResult = result.Value as OrderOut;

            ordersLogicMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.IsTrue(expectedObject.Equals(objectResult));
        }
    }
}
