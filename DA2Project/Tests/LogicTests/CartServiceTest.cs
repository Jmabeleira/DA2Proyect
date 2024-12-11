using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.WebModels.In;
using Logic.Exceptions;
using Logic.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Moq;
using Promo20OffDlls.Models;
using Promo3X2Dlls.Models;

namespace Tests.LogicTests
{
    [TestClass]
    public class CartServiceTest
    {
        private ICartService _cartService;

        [TestMethod]
        public void TestGetCartByIdValid()
        {
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
                        Name = "Pants",
                        Description = "Cool Pants",
                        Price = 2000,
                        Brand = "Levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas"
                        }
                    }
                },
                Promotion = new Promo20Off()
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Returns(cart);
            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Cart expected = cart;
            Cart result = _cartService.GetCartById(cart.Id);

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetCartByIdInvalid()
        {
            var invalidId = Guid.NewGuid();

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);
            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Throws(new NoInstanceException(It.IsAny<string>()));
            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.GetCartById(invalidId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("Cart does not exist"));
        }

        [TestMethod]
        public void TestGetCartByUserIdValid()
        {
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
                        Name = "Pants",
                        Description = "Cool Pants",
                        Price = 2000,
                        Brand = "Levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas"
                        }
                    }
                },
                Promotion = new Promo20Off()
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Returns(cart);

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Cart expected = cart;
            Cart result = _cartService.GetCartByUserId(cart.User.Id);

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetCartByUserIdInvalid()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                User = new User { Id = userId },
                Products = new List<CartProduct>() { }
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Throws(new NoInstanceException(It.IsAny<string>()));
            cartRepositoryMock.Setup(repo => repo.AddCart(It.IsAny<Cart>())).Returns(cart);

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Cart expected = cart;
            Cart result = _cartService.GetCartByUserId(userId);

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddCartValid()
        {
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
                        Name = "Pants",
                        Description = "Cool Pants",
                        Price = 2000,
                        Brand = "Levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas"
                        }
                    }
                },
                Promotion = new Promo20Off()
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.AddCart(It.IsAny<Cart>())).Returns(cart);
            promoServiceMock.Setup(promo => promo.ApplyPromo(It.IsAny<List<CartProduct>>())).Returns(new Promo3X2());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Cart expected = cart;
            Cart result = _cartService.AddCart(cart);

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddCartInvalid()
        {
            Cart invalidCart = new Cart();

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.AddCart(It.IsAny<Cart>())).Throws(new InstanceNotUniqueException(It.IsAny<string>()));
            promoServiceMock.Setup(promo => promo.ApplyPromo(It.IsAny<List<CartProduct>>())).Returns(new Promo3X2());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.AddCart(invalidCart);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(AlreadyExistsException));
            Assert.IsTrue(exception.Message.Equals("Cart already exists"));
        }

        [TestMethod]
        public void TestDeleteCartByIdValid()
        {
            Cart cartDeleted = new Cart
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
                        Name = "Pants",
                        Description = "Cool Pants",
                        Price = 2000,
                        Brand = "Levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas"
                        }
                    }
                },
                Promotion = new Promo20Off()
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.DeleteCartById(It.IsAny<Guid>())).Returns(true);
            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            bool expected = true;
            bool result = _cartService.DeleteCartById(cartDeleted.Id);

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDeleteCartByIdInvalid()
        {
            var invalidId = Guid.NewGuid();

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.DeleteCartById(It.IsAny<Guid>())).Throws(new NoInstanceException(It.IsAny<string>()));

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                bool result = _cartService.DeleteCartById(invalidId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("Cart does not exist"));
        }

        [TestMethod]
        public void TestModifyCartByIdValid()
        {
            Cart cartModified = new Cart
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
                        Name = "Pants",
                        Description = "Cool Pants",
                        Price = 2000,
                        Brand = "Levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas"
                        }
                    }
                },
                Promotion = new Promo20Off()
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.ModifyCart(It.IsAny<Cart>())).Returns(cartModified);
            promoServiceMock.Setup(promo => promo.ApplyPromo(It.IsAny<List<CartProduct>>())).Returns(new Promo20Off());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Cart expected = cartModified;
            Cart result = _cartService.ModifyCart(cartModified);

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestModifyCartByIdInvalid()
        {
            Cart invalidCart = new Cart();

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.ModifyCart(It.IsAny<Cart>())).Throws(new NoInstanceException(It.IsAny<string>()));
            promoServiceMock.Setup(promo => promo.ApplyPromo(It.IsAny<List<CartProduct>>())).Returns(new Promo20Off());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.ModifyCart(invalidCart);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("Cart does not exist"));
        }

        [TestMethod]
        public void TestAddProductToCartValid()
        {
            Cart cartModified = new Cart
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
                        Name = "Pants",
                        Description = "Cool Pants",
                        Price = 2000,
                        Brand = "Levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas",
                        Stock = 2
                        },
                        Quantity=1
                    },
                    new CartProduct
                    {
                        Product=new Product
                        {
                        Name = "Pants2",
                        Description = "Cool Pants2",
                        Price = 2020,
                        Brand = "Levis2",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas"
                        },
                        Quantity=1
                    }
                },
                Promotion = new Promo20Off()
            };
            var newProd = new Product
            {
                Name = "Pants2",
                Description = "Cool Pants2",
                Price = 2020,
                Brand = "Levis2",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 2
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Returns(cartModified);
            cartRepositoryMock.Setup(repo => repo.ModifyCart(It.IsAny<Cart>())).Returns(cartModified);
            promoServiceMock.Setup(promo => promo.ApplyPromo(It.IsAny<List<CartProduct>>())).Returns(new Promo20Off());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Cart expected = cartModified;
            Cart result = _cartService.AddProductToCart(cartModified.Id, newProd);

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddProductToCartInvalid()
        {
            Guid invalidCartId = Guid.NewGuid();

            var newProd = new Product
            {
                Name = "Pants2",
                Description = "Cool Pants2",
                Price = 2020,
                Brand = "Levis2",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 2
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Throws(new NoInstanceException(It.IsAny<string>()));

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.AddProductToCart(invalidCartId, newProd);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("Cart does not exist"));
        }

        [TestMethod]
        public void TestAddProductWithoutStockException()
        {
            Cart cartModified = new Cart
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
                        Name = "Pants",
                        Description = "Cool Pants",
                        Price = 2000,
                        Brand = "Levis",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas",
                        Stock = 1
                        },
                        Quantity=1
                    },
                    new CartProduct
                    {
                        Product=new Product
                        {
                        Name = "Pants2",
                        Description = "Cool Pants2",
                        Price = 2020,
                        Brand = "Levis2",
                        Colors = new List<string> { "red", "blue" },
                        Category = "Bebidas gaseosas",
                        Stock = 1
                        },
                        Quantity=1
                    }
                },
                Promotion = new Promo20Off()
            };

            var newProd = new Product
            {
                Name = "Pants2",
                Description = "Cool Pants2",
                Price = 2020,
                Brand = "Levis2",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 0
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.AddProductToCart(cartModified.Id, newProd);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(NoStockException));
            Assert.IsTrue(exception.Message.Equals("Product has no Stock"));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetCart()
        {
            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);
            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Throws(new Exception());
            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.GetCartById(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtAddCart()
        {
            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);
            cartRepositoryMock.Setup(repo => repo.AddCart(It.IsAny<Cart>())).Throws(new Exception());
            promoServiceMock.Setup(promo => promo.ApplyPromo(It.IsAny<List<CartProduct>>())).Returns(new Promo3X2());
            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.AddCart(new Cart());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionAtDeleteCart()
        {
            var invalidId = Guid.NewGuid();

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.DeleteCartById(It.IsAny<Guid>())).Throws(new Exception());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                bool result = _cartService.DeleteCartById(invalidId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtModifyCart()
        {
            Cart invalidCart = new Cart();

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.ModifyCart(It.IsAny<Cart>())).Throws(new Exception());
            promoServiceMock.Setup(promo => promo.ApplyPromo(It.IsAny<List<CartProduct>>())).Returns(new Promo20Off());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.ModifyCart(invalidCart);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtAddProductToCart()
        {
            Guid invalidCartId = Guid.NewGuid();

            var newProd = new Product
            {
                Name = "Pants2",
                Description = "Cool Pants2",
                Price = 2020,
                Brand = "Levis2",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas",
                Stock = 2
            };

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Throws(new Exception());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.AddProductToCart(invalidCartId, newProd);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetCartByUserId()
        {
            var invalidId = Guid.NewGuid();

            Mock<ICartRepository> cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
            Mock<IPromoService> promoServiceMock = new Mock<IPromoService>(MockBehavior.Strict);

            cartRepositoryMock.Setup(repo => repo.GetCart(It.IsAny<Func<Cart, bool>>())).Throws(new Exception());

            _cartService = new CartService(cartRepositoryMock.Object, promoServiceMock.Object);

            Exception exception = null;

            try
            {
                Cart result = _cartService.GetCartByUserId(invalidId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartRepositoryMock.VerifyAll();
            promoServiceMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }
    }
}
