using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Logic.Exceptions;
using Logic.Services;
using Moq;

namespace Tests.LogicTests
{
    [TestClass]
    public class ProductServiceTest
    {
        IProductService _productService;

        [TestMethod]
        public void TestGetProductByIdOk()
        {
            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            Product expectedProduct = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = colors,
                Price = 10.0,
                IsPromotional = true
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetProductById(expectedProduct.Id)).Returns(expectedProduct);
            _productService = new ProductService(productRepositoryMock.Object);

            Product actualProduct = _productService.GetProductById(expectedProduct.Id);

            productRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedProduct, actualProduct);
        }


        [TestMethod]
        public void TestGetProductByIdInvalid()
        {
            var falseId = Guid.NewGuid();
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetProductById(falseId)).Throws(new NoInstanceException(It.IsAny<string>()));
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.GetProductById(falseId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("There is no such Product"));
        }

        [TestMethod]
        public void TestGetAllProductsOk()
        {
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TestProduct",
                    Description = "TestDescription",
                    Brand = "TestBrand",
                    Colors = new List<string>{"red"},
                    Price = 10.0,
                    IsPromotional = true
                }
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);
            _productService = new ProductService(productRepositoryMock.Object);

            IEnumerable<Product> actualProducts = _productService.GetAllProducts(new ParametersFilter(null, null, null, true, null, null));

            productRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedProducts.SequenceEqual(actualProducts));
        }

        [TestMethod]
        public void TestGetAllProductsInvalid()
        {
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Throws(new NoInstanceException(It.IsAny<string>()));
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.GetAllProducts(new ParametersFilter(null, null, null, null, null, null));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoContentException));
            Assert.IsTrue(exception.Message.Equals("There are no Products"));
        }

        [TestMethod]
        public void TestAddProductOk()
        {
            Product expectedProduct = new Product()
            {
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = new List<string> { "Rojo", "Azul", "Verde" },
                Price = 10.0,
                IsPromotional = true
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.AddProduct(expectedProduct)).Returns(expectedProduct);
            _productService = new ProductService(productRepositoryMock.Object);

            Product actualProduct = _productService.AddProduct(expectedProduct);

            productRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedProduct, actualProduct);

        }

        [TestMethod]
        public void TestAddProductInvalid()
        {
            Product expectedProduct = new Product()
            {
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = new List<string> { "Rojo", "Azul", "Verde" },
                Price = 10.0,
                IsPromotional = true
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.AddProduct(expectedProduct)).Throws(new InstanceNotUniqueException(It.IsAny<string>()));
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.AddProduct(expectedProduct);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(AlreadyExistsException));
            Assert.IsTrue(exception.Message.Equals("Product already exists"));
        }

        [TestMethod]
        public void TestDeleteProductOk()
        {
            var expectedId = Guid.NewGuid();

            Product expectedProduct = new Product()
            {
                Id = expectedId,
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = new List<string> { "Rojo", "Azul", "Verde" },
                Price = 10.0,
                IsPromotional = true
            };
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.DeleteProductById(expectedId));
            _productService = new ProductService(productRepositoryMock.Object);

            var result = _productService.DeleteProductById(expectedId);

            productRepositoryMock.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestDelteProductInvalid()
        {
            var falseId = Guid.NewGuid();
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.DeleteProductById(falseId)).Throws(new NoInstanceException(It.IsAny<string>()));
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.DeleteProductById(falseId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("There is no such Product"));
        }

        [TestMethod]
        public void TestUpdateProductOk()
        {
            var expectedId = Guid.NewGuid();
            Product expectedProduct = new Product()
            {
                Id = expectedId,
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = new List<string> { "Rojo", "Azul", "Verde" },
                Price = 10.0,
                IsPromotional = true
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.UpdateProduct(expectedProduct)).Returns(expectedProduct);
            _productService = new ProductService(productRepositoryMock.Object);

            Product actualProduct = _productService.UpdateProduct(expectedProduct);
            Assert.AreEqual(expectedProduct, actualProduct);
        }


        [TestMethod]
        public void TestUpdateProductInvalid()
        {
            var expectedId = Guid.NewGuid();

            Product expectedProduct = new Product()
            {
                Id = expectedId,
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = new List<string> { "Rojo", "Azul", "Verde" },
                Price = 10.0,
                IsPromotional = true
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.UpdateProduct(expectedProduct)).Throws(new NoInstanceException(It.IsAny<string>()));
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.UpdateProduct(expectedProduct);
            }
            catch (Exception ex)
            {

                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("There is no such Product"));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetProductById()
        {
            var falseId = Guid.NewGuid();
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetProductById(falseId)).Throws(new Exception());
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.GetProductById(falseId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetAllProducts()
        {
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Throws(new Exception());
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.GetAllProducts(new ParametersFilter(null, null, null, null, null, null));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtAddProduct()
        {
            Product expectedProduct = new Product()
            {
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = new List<string> { "Rojo", "Azul", "Verde" },
                Price = 10.0,
                IsPromotional = true
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.AddProduct(expectedProduct)).Throws(new Exception());
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.AddProduct(expectedProduct);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtDeleteProduct()
        {
            var falseId = Guid.NewGuid();
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.DeleteProductById(falseId)).Throws(new Exception());
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.DeleteProductById(falseId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtUpdateProduct()
        {
            var expectedId = Guid.NewGuid();

            Product expectedProduct = new Product()
            {
                Id = expectedId,
                Name = "TestProduct",
                Description = "TestDescription",
                Brand = "TestBrand",
                Colors = new List<string> { "Rojo", "Azul", "Verde" },
                Price = 10.0,
                IsPromotional = true
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.UpdateProduct(expectedProduct)).Throws(new Exception());
            _productService = new ProductService(productRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _productService.UpdateProduct(expectedProduct);
            }
            catch (Exception ex)
            {

                exception = ex;
            }

            productRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestGetAllProductsWithTextFilterOk()
        {
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TestProduct",
                    Description = "TestDescription",
                    Brand = "TestBrand",
                    Colors = new List<string>{"red"},
                    Price = 10.0,
                    IsPromotional = true
                }
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);
            _productService = new ProductService(productRepositoryMock.Object);

            IEnumerable<Product> actualProducts = _productService.GetAllProducts(new ParametersFilter("TestProduct", null, null, null, null, null));

            productRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedProducts.SequenceEqual(actualProducts));
        }

        [TestMethod]
        public void TestGetAllProductsWithBrandFilterOk()
        {
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TestProduct",
                    Description = "TestDescription",
                    Brand = "TestBrand",
                    Colors = new List<string>{"red"},
                    Price = 10.0,
                    IsPromotional = true
                }
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);
            _productService = new ProductService(productRepositoryMock.Object);

            IEnumerable<Product> actualProducts = _productService.GetAllProducts(new ParametersFilter(null, "TestBrand", null, null, null, null));

            productRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedProducts.SequenceEqual(actualProducts));
        }

        [TestMethod]
        public void TestGetAllProductsWithCategoryFilterOk()
        {
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TestProduct",
                    Description = "TestDescription",
                    Brand = "TestBrand",
                    Category = "Product",
                    Colors = new List<string>{"red"},
                    Price = 10.0,
                    IsPromotional = true
                }
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);
            _productService = new ProductService(productRepositoryMock.Object);

            IEnumerable<Product> actualProducts = _productService.GetAllProducts(new ParametersFilter(null, null, "Product", null, null, null));

            productRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedProducts.SequenceEqual(actualProducts));
        }

        [TestMethod]
        public void TestGetAllProductsWithMinPriceFilterOk()
        {
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TestProduct",
                    Description = "TestDescription",
                    Brand = "TestBrand",
                    Category = "Product",
                    Colors = new List<string>{"red"},
                    Price = 10.0,
                    IsPromotional = true
                }
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);
            _productService = new ProductService(productRepositoryMock.Object);

            IEnumerable<Product> actualProducts = _productService.GetAllProducts(new ParametersFilter(null, null, null, null, 5, null));

            productRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedProducts.SequenceEqual(actualProducts));
        }

        [TestMethod]
        public void TestGetAllProductsWithMaxPriceFilterOk()
        {
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "TestProduct",
                    Description = "TestDescription",
                    Brand = "TestBrand",
                    Category = "Product",
                    Colors = new List<string>{"red"},
                    Price = 10.0,
                    IsPromotional = true
                }
            };

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepositoryMock.Setup(p => p.GetAllProducts()).Returns(expectedProducts);
            _productService = new ProductService(productRepositoryMock.Object);

            IEnumerable<Product> actualProducts = _productService.GetAllProducts(new ParametersFilter(null, null, null, null, null, 20));

            productRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedProducts.SequenceEqual(actualProducts));
        }
    }
}
