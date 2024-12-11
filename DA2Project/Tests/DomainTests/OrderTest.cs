using Domain.Models;
using Domain.Exceptions;
using Domain.Models.PaymentMethods;

namespace Tests.DomainTests
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void TestProductsListNotEmpty()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Exception exception = null;
            try
            {
                Order order = new Order
                {
                    Customer = customer,
                    Products = new List<CartProduct> { },
                    Date = new DateTime(2023, 9, 9, 12, 0, 0),
                    PaymentMethod = new PayPal()
                };
            }
            catch (Exception ex) { exception = ex; }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The products list cannot be empty"));
        }

        [TestMethod]
        public void TestNullCustomer()
        {
            User customer = null;

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

            Exception exception = null;
            try
            {
                Order order = new Order
                {
                    Customer = customer,
                    Products = new List<CartProduct> { product },
                    Date = new DateTime(2023, 9, 9, 12, 0, 0),
                    PaymentMethod = new CreditCard() { CardType = CreditCardType.Visa }
                };
            }
            catch (Exception ex) { exception = ex; }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The customer cannot be null"));
        }

        [TestMethod]
        public void TestInvalidDate()
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

            Exception exception = null;

            try
            {
                Order order = new Order
                {
                    Customer = customer,
                    Products = new List<CartProduct> { product },
                    Date = DateTime.MinValue,
                    PaymentMethod = new DebitCard() { CardType = DebitCardType.Santander }
                };
            }
            catch (Exception ex) { exception = ex; }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("Invalid datetime"));
        }

        [TestMethod]
        public void TestPaymentMethodInvalid()
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

            Exception exception = null;

            try
            {
                Order order = new Order
                {
                    Customer = customer,
                    Products = new List<CartProduct> { product },
                    Date = new DateTime(2023, 11, 15, 12, 30, 0),
                    PaymentMethod = null
                };
            }
            catch (Exception ex) { exception = ex; }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("Payment Method can not be null"));
        }

        [TestMethod]
        public void TestProductsListNull()
        {
            User customer = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Exception exception = null;
            try
            {
                Order order = new Order
                {
                    Customer = customer,
                    Products = null,
                    Date = new DateTime(2023, 9, 9, 12, 0, 0),
                    PaymentMethod = new PayPal()
                };
            }
            catch (Exception ex) { exception = ex; }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The products list cannot be null"));
        }
    }
}
