using DataAccess.Repositories;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.PaymentMethods;
using Domain.WebModels.Out;
using Dtos.Interfaces;
using Dtos.Mappers;
using Dtos.Models;
using Logic.Exceptions;
using Logic.Services;
using Moq;
using Moq.EntityFrameworkCore;
using Promo20OffDlls.Models;

namespace Tests.LogicTests
{
    [TestClass]
    public class PurchaseServiceTest
    {
        IPurchaseService purchaseService;

        [TestMethod]
        public void TestPurchaseOk()
        {
            var product = new Product
            {
                Name = "Pants",
                Description = "Cool Pants",
                Price = 2000,
                Brand = "Levis",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 3
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
                        Product = product,
                        Quantity = 1,
                    }
                },
                Promotion = new Promo20Off()
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

            IProductRepository productRepository;
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);
            IProductService productService = new ProductService(productRepository);

            purchaseService = new PurchaseService(productService);

            Order order = purchaseService.Purchase(cartToOrder, PaymentKey.PayPal);

            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void TestDeleteProductWithoutStock()
        {
            var product = new Product
            {
                Name = "Pants",
                Description = "Cool Pants",
                Price = 2000,
                Brand = "Levis",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 3
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
                        Product = product,
                        Quantity = 3,
                    }
                },
                Promotion = new Promo20Off()
            };

            Cart cart2 = new Cart
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
                        Product = product,
                        Quantity = 3,
                    }
                },
                Promotion = new Promo20Off()
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

            var cartToOrder2 = new Order
            {
                Id = cartOut.Id,
                Customer = cart2.User,
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

            IProductRepository productRepository;
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);
            IProductService productService = new ProductService(productRepository);

            purchaseService = new PurchaseService(productService);

            Order order = purchaseService.Purchase(cartToOrder, PaymentKey.PayPal);

            Exception ex = null;

            try
            {
                Order order2 = purchaseService.Purchase(cartToOrder2, PaymentKey.PayPal);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(NoStockException));
        }

        [TestMethod]
        public void TestPaymentMethodDoesNotExistException()
        {
            var product = new Product
            {
                Name = "Pants",
                Description = "Cool Pants",
                Price = 2000,
                Brand = "Levis",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 3
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
                        Product = product,
                        Quantity = 1,
                    }
                },
                Promotion = new Promo20Off()
            };

            Cart cart2 = new Cart
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
                        Product = product,
                        Quantity = 3,
                    }
                },
                Promotion = new Promo20Off()
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

            IProductRepository productRepository;
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);
            IProductService productService = new ProductService(productRepository);
            Exception exception = null;

            purchaseService = new PurchaseService(productService);

            try
            {
                Order order = purchaseService.Purchase(cartToOrder, "asdasd");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(PaymentMethodException));
            Assert.IsTrue(exception.Message.Equals("Payment method was rejected"));
        }

        [TestMethod]
        public void TestPurchaseWithPaganzaOk()
        {
            var product = new Product
            {
                Name = "Pants",
                Description = "Cool Pants",
                Price = 2000,
                Brand = "Levis",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 3
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
                        Product = product,
                        Quantity = 1,
                    }
                },
                Promotion = new Promo20Off()
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

            IProductRepository productRepository;
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);
            IProductService productService = new ProductService(productRepository);

            purchaseService = new PurchaseService(productService);

            Order order = purchaseService.Purchase(cartToOrder, PaymentKey.Paganza);

            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void TestPurchaseWithDebitCardOk()
        {
            var product = new Product
            {
                Name = "Pants",
                Description = "Cool Pants",
                Price = 2000,
                Brand = "Levis",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 3
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
                        Product = product,
                        Quantity = 1,
                    }
                },
                Promotion = new Promo20Off()
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

            IProductRepository productRepository;
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);
            IProductService productService = new ProductService(productRepository);

            purchaseService = new PurchaseService(productService);

            Order order = purchaseService.Purchase(cartToOrder, PaymentKey.DebitCardBbva);

            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void TestPurchaseWithCreditCardOk()
        {
            var product = new Product
            {
                Name = "Pants",
                Description = "Cool Pants",
                Price = 2000,
                Brand = "Levis",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 3
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
                        Product = product,
                        Quantity = 1,
                    }
                },
                Promotion = new Promo20Off()
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

            IProductRepository productRepository;
            var productContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            productContextMock.Setup(x => x.Products).ReturnsDbSet(new List<ProductDto> { ProductHelper.FromProductToProductDto(product) });
            productContextMock.Setup(x => x.SaveChanges()).Returns(1);
            productRepository = new ProductRepository(productContextMock.Object);
            IProductService productService = new ProductService(productRepository);

            purchaseService = new PurchaseService(productService);

            Order order = purchaseService.Purchase(cartToOrder, PaymentKey.CreditCardVisa);

            Assert.IsNotNull(order);
        }
    }
}
