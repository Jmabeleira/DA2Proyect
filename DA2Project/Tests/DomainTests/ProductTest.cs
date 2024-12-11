using Domain.Exceptions;
using Domain.Models;

namespace Tests.DomainTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void TestProductCreationOk()
        {
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category",
                IsPromotional = true
            };

            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void TestProductNameIsNotValid()
        {
            Exception exception = null;

            try
            {
                var product = new Product
                {
                    Name = string.Empty,
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                };

            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The product's name cannot be empty"));
        }

        [TestMethod]
        public void TestProductPriceIsPositiveValue()
        {
            Exception exception = null;

            try
            {
                var product = new Product
                {
                    Name = "Product",
                    Price = -100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                };

            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The product's price must be greater or equal to 0"));
        }

        [TestMethod]
        public void TestEmptyDescription()
        {
            Exception exception = null;

            try
            {
                var product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = string.Empty,
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                };

            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The product's description cannot be empty"));
        }

        [TestMethod]
        public void TestCartProductIsNotValid()
        {
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };
            var quantity = -1;

            Assert.ThrowsException<RequestValidationException>(() => new CartProduct
            {
                Product = product,
                Quantity = quantity
            });
        }

        [TestMethod]
        public void TestBrandIsNotValid()
        {
            Exception exception = null;

            try
            {
                var product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = string.Empty,
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The product's brand cannot be empty"));
        }

        [TestMethod]
        public void TestColorsNotValid()
        {
            Exception exception = null;

            try
            {
                var product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { },
                    Category = "Category"
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("Colors cannot be null or empty"));
        }

        [TestMethod]
        public void TestCategoryNotValid()
        {
            Exception exception = null;

            try
            {
                var product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "red", "blue" },
                    Category = string.Empty
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("Category cannot be null or empty"));
        }

        [TestMethod]
        public void TestStockInvalid()
        {
            Exception exception = null;

            try
            {
                var product = new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "red", "blue" },
                    Category = "Category",
                    Stock = -1
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("Stock cannot be lower than 0"));
        }
    }
}
