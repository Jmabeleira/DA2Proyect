using DataAccess.Exceptions;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Dtos.Interfaces;
using Dtos.Mappers;
using Dtos.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace Tests.DataAccessTests
{
    [TestClass]
    public class ProductDataAccessTest
    {
        IProductRepository productRepository;

        [TestMethod]
        public void TestAddProductOk()
        {
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = new List<string> { "red", "blue" },
                Category = "T-Shirts"
            };

            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);

            var result = productRepository.AddProduct(product);
            Assert.IsNotNull(result);
            Assert.AreEqual(product, result);
        }

        [TestMethod]
        public void TestAddProductInvalid()
        {
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = new List<string> { "red", "blue" },
                Category = "T-Shirts"
            };

            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productRepository = new ProductRepository(productContextMock.Object);
            Exception exception = null;

            try
            {
                productRepository.AddProduct(product);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(InstanceNotUniqueException));
            Assert.AreEqual("Product already exists", exception.Message);
        }

        [TestMethod]
        public void TestGetAllProductsOk()
        {
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = new List<string> { "red", "blue" },
                Category = "T-Shirts"
            };

            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productRepository = new ProductRepository(productContextMock.Object);

            var result = productRepository.GetAllProducts();
            Assert.IsNotNull(result);
            Assert.AreEqual(product, result.First());
        }

        [TestMethod]
        public void TestGetAllProductsInvalid()
        {
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { });
            productRepository = new ProductRepository(productContextMock.Object);

            var result = productRepository.GetAllProducts();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void TestGetProductByIdOk()
        {
            Guid id = Guid.NewGuid();
            Product product = new Product
            {
                Id = id,
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = new List<string> { "red", "blue" },
                Category = "T-Shirts"
            };
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productRepository = new ProductRepository(productContextMock.Object);

            var result = productRepository.GetProductById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(product, result);
        }

        [TestMethod]
        public void TestGetProductByIdInvalid()
        {
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { });
            productRepository = new ProductRepository(productContextMock.Object);
            Exception exception = null;

            try
            {
                productRepository.GetProductById(Guid.NewGuid());
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("Product not found", exception.Message);
        }

        [TestMethod]
        public void TestDelteProductByIdOk()
        {
            Guid id = Guid.NewGuid();
            Product product = new Product
            {
                Id = id,
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = new List<string> { "red", "blue" },
                Category = "T-Shirts"
            };

            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);

            productRepository.DeleteProductById(id);
            var expectedProduct = productContextMock.Object.Products.Select((x => ProductHelper.FromProductDtoToProduct(x)));

            Assert.IsNotNull(expectedProduct);
            Assert.IsTrue(expectedProduct.Contains(product));
        }

        [TestMethod]
        public void TestDeleteProductByIdInvalid()
        {
            Guid id = Guid.NewGuid();
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { });
            productRepository = new ProductRepository(productContextMock.Object);
            Exception exception = null;

            try
            {
                productRepository.DeleteProductById(id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("Product not found", exception.Message);
        }


        [TestMethod]
        public void TestUpdateProductOk()
        {
            Guid id = Guid.NewGuid();
            Product product = new Product
            {
                Id = id,
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = new List<string> { "red", "blue" },
                Category = "T-Shirts"
            };

            var productDto = ProductHelper.FromProductToProductDto(product);

            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { productDto });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);

            productRepository = new ProductRepository(productContextMock.Object);

            product.Name = "Jaegerists Shirt";
            product.Brand = "AotBrand";
            var productResult = productRepository.UpdateProduct(product);

            productContextMock.Verify(x => x.Products);

            Assert.IsNotNull(productResult);
            Assert.AreEqual(product, productResult);
        }

        [TestMethod]
        public void TestsUpdateProductOk()
        {
            Guid id = Guid.NewGuid();
            Product product = new Product
            {
                Id = id,
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = new List<string> { "red", "blue" },
                Category = "T-Shirts"
            };

            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { });
            productRepository = new ProductRepository(productContextMock.Object);
            Exception exception = null;

            try
            {
                productRepository.UpdateProduct(product);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("Product not found", exception.Message);
        }

        [TestMethod]
        public void TestUnhandledDataAccessExceptionThrown()
        {
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).Throws(new Exception());
            productRepository = new ProductRepository(productContextMock.Object);
            Exception exception = null;

            try
            {
                var result = productRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            productContextMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledDataAccessException));
            Assert.IsTrue(exception.Message.Equals("A DataAccess error has ocurred: " + exception.InnerException.Message));
        }
    }
}
