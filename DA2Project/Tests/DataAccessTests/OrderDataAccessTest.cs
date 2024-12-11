using DataAccess.Exceptions;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.PaymentMethods;
using Dtos.Interfaces;
using Dtos.Mappers;
using Dtos.Models;
using Moq;
using Moq.EntityFrameworkCore;
using Promo20OffDlls.Models;

namespace Tests.DataAccessTests
{
    [TestClass]
    public class OrderDataAccessTest
    {
        IOrderRepository _orderRepository;
        Promotion promotion = new Promo20Off();

        [TestMethod]
        public void TestGetAllOrdersOk()
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
                PaymentMethod = new PayPal()
            };

            var orderContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            orderContextMock.Setup(x => x.Orders).ReturnsDbSet(new List<OrderDto> { OrderHelper.FromOrderToOrderDto(order) });
            _orderRepository = new OrderRepository(orderContextMock.Object);

            var result = _orderRepository.GetAllOrders();
            result.ForEach(o => o.AppliedPromotion = new AppliedPromotion()
            {
                Name = promotion.Name,
                Description = promotion.Description,
                Condition = promotion.Condition
            });

            orderContextMock.Verify(x => x.Orders);
            Assert.IsNotNull(result);
            Assert.IsTrue(order.Equals(result.First()));
        }

        [TestMethod]
        public void TestGetAllOrdersInvalid()
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
            };

            var orderContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            orderContextMock.Setup(x => x.Orders).ReturnsDbSet(new List<OrderDto> { });
            _orderRepository = new OrderRepository(orderContextMock.Object);

            Exception exception = null;

            try
            {
                _orderRepository.GetAllOrders();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.IsTrue(exception.Message.Equals("No orders found"));
        }

        [TestMethod]
        public void TestAddOrderOk()
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
                PaymentMethod = new PayPal()
            };

            var orderDto = OrderHelper.FromOrderToOrderDto(order);

            var orderContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            orderContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { orderDto.Customer });
            orderContextMock.Setup(x => x.Products).ReturnsDbSet(orderDto.Products.Select(x => x.Product).ToList());
            orderContextMock.Setup(x => x.Orders).ReturnsDbSet(new List<OrderDto> { });
            orderContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _orderRepository = new OrderRepository(orderContextMock.Object);

            var result = _orderRepository.AddOrder(order);

            orderContextMock.Verify(x => x.Orders);
            Assert.IsNotNull(result);
            Assert.IsTrue(order.Equals(result));
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
                AppliedPromotion = new AppliedPromotion()
                {
                    Name = promotion.Name,
                    Description = promotion.Description,
                    Condition = promotion.Condition
                },
                PaymentMethod = new PayPal()
            };

            var orderContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            orderContextMock.Setup(x => x.Orders).ReturnsDbSet(new List<OrderDto> { OrderHelper.FromOrderToOrderDto(order) });
            _orderRepository = new OrderRepository(orderContextMock.Object);

            Exception exception = null;

            try
            {
                _orderRepository.AddOrder(order);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderContextMock.Verify(x => x.Orders);
            Assert.IsInstanceOfType(exception, typeof(InstanceNotUniqueException));
            Assert.IsTrue(exception.Message.Equals("Order already exists"));
        }

        [TestMethod]
        public void TestUnhandledDataAccessExceptionThrown()
        {
            var orderContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            orderContextMock.Setup(x => x.Orders).Throws(new Exception());
            _orderRepository = new OrderRepository(orderContextMock.Object);
            Exception exception = null;

            try
            {
                var result = _orderRepository.GetAllOrders();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            orderContextMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledDataAccessException));
            Assert.IsTrue(exception.Message.Equals("A DataAccess error has ocurred: " + exception.InnerException.Message));
        }
    }
}
