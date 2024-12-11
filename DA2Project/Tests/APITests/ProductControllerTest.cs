using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.WebModels.In;
using ECommerceAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.APITests
{

    [TestClass]
    public class ProductControllerTest
    {
        private ProductsController _productController;

        [TestMethod]
        public void AddProductOk()
        {
            User userAuth = new User
            {
                Email = "eren1@gmail.com",
                UserRole = new List<UserRole> { UserRole.Admin },
                Password = "piripitiflauticoMagico",
                Address = new Address
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            ProductIn productIn = new ProductIn
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts"
            };

            Product product = productIn.ToEntity();

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.Setup(x => x.Items["user"]).Returns(userAuth);
            productLogicMock.Setup(logic => logic.AddProduct(It.IsAny<Product>())).Returns(product);

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            CreatedAtActionResult expected = new CreatedAtActionResult(nameof(ProductsController.AddProduct), nameof(ProductsController), null, product);
            CreatedAtActionResult result = _productController.AddProduct(productIn) as CreatedAtActionResult;
            Product objectResult = result.Value as Product;

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode)
                                                                        && result.Value.Equals(expected.Value));
        }

        [TestMethod]
        public void AddProductException()
        {
            User unauthorizedUser = new User
            {
                Email = "eren1@gmail.com",
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
            };

            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            ProductIn productIn = new ProductIn
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts"
            };

            Product product = productIn.ToEntity();

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.Setup(x => x.Items["user"]).Returns(unauthorizedUser);

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _productController.AddProduct(productIn);
            }
            catch (Exception e)
            {
                ex = e;
            }

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void GetAllProductsOk()
        {
            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts",
                IsPromotional = true
            };

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            productLogicMock.Setup(logic => logic.GetAllProducts(It.IsAny<ParametersFilter>())).Returns(new List<Product> { product });

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<Product> { product });
            OkObjectResult result = _productController.GetAllProducts(null, null, null, null, null, null) as OkObjectResult;
            List<Product> objectResult = result.Value as List<Product>;

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode)
                                                         && objectResult.Count.Equals(1));
        }

        [TestMethod]
        public void GetProductByIdOk()
        {
            User userAuth = new User
            {
                Email = "eren1@gmail.com",
                UserRole = new List<UserRole> { UserRole.Admin },
                Password = "piripitiflauticoMagico",
                Address = new Address
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts"
            };

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            productLogicMock.Setup(logic => logic.GetProductById(product.Id)).Returns(product);

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(product);
            OkObjectResult result = _productController.GetProductById(product.Id) as OkObjectResult;
            Product objectResult = result.Value as Product;

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode)
                                                                        && objectResult.Id.Equals(product.Id));
        }

        [TestMethod]
        public void UpdateProductOk()
        {
            User userAuth = new User
            {
                Email = "eren1@gmail.com",
                UserRole = new List<UserRole> { UserRole.Admin },
                Password = "piripitiflauticoMagico",
                Address = new Address
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts"
            };

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.Setup(x => x.Items["user"]).Returns(userAuth);
            productLogicMock.Setup(logic => logic.UpdateProduct(product)).Returns(product);

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(product);
            OkObjectResult result = _productController.ModifyProductById(product) as OkObjectResult;
            Product objectResult = result.Value as Product;

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode)
                                           && objectResult.Id.Equals(product.Id));
        }

        [TestMethod]
        public void UpdateProductUnAuthorized()
        {
            User unauthorizedUser = new User
            {
                Email = "eren1@gmail.com",
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
            };

            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts"
            };

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.Setup(x => x.Items["user"]).Returns(unauthorizedUser);

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _productController.ModifyProductById(product);
            }
            catch (Exception e)
            {
                ex = e;
            }

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void DeleteProductOk()
        {
            User userAuth = new User
            {
                Email = "eren1@gmail.com",
                UserRole = new List<UserRole> { UserRole.Admin },
                Password = "piripitiflauticoMagico",
                Address = new Address
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts"
            };

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.Setup(x => x.Items["user"]).Returns(userAuth);
            productLogicMock.Setup(logic => logic.DeleteProductById(It.IsAny<Guid>())).Returns(true);

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult("Removed product Succesfully");
            OkObjectResult result = _productController.DeleteProductById(product.Id) as OkObjectResult;

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
        }

        [TestMethod]
        public void DeleteProductUnAuthorized()
        {
            User unauthorizedUser = new User
            {
                Email = "eren1@gmail.com",
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
            };

            IEnumerable<string> colors = new List<string> { "Rojo", "Azul", "Verde" };
            Product product = new Product
            {
                Name = "T-Shirt Relaxed Fit",
                Price = 299,
                Description = "New T-Shirt with many colors",
                Brand = "H&M",
                Colors = colors,
                Category = "T-Shirts"
            };

            Mock<IProductService> productLogicMock = new Mock<IProductService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.Setup(x => x.Items["user"]).Returns(unauthorizedUser);

            _productController = new ProductsController(productLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _productController.DeleteProductById(product.Id);
            }
            catch (Exception e)
            {
                ex = e;
            }

            productLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }
    }
}
