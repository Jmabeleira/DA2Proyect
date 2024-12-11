using Domain.Models;
using Domain.Interfaces;
using Moq;
using Dtos.Interfaces;
using DataAccess.Repositories;
using DataAccess.Exceptions;
using PromoFidelityDlls.Models;
using Promo20OffDlls.Models;

namespace Tests.DataAccessTests
{
    [TestClass]
    public class CartDataAccessTest
    {
        ICartRepository _cartRepository;

        [TestMethod]
        public void TestAddCartOk()
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

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(x => x.AddCart(cart)).Returns(true);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart>());
            _cartRepository = new CartRepository(cartContextMock.Object);

            var result = _cartRepository.AddCart(cart);

            cartContextMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(cart, result);
        }

        [TestMethod]
        public void TestAddCartInvalid()
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

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart> { cart });
            _cartRepository = new CartRepository(cartContextMock.Object);

            Exception exception = null;

            try
            {
                _cartRepository.AddCart(cart);
            }
            catch (Exception e)
            {
                exception = e;
            }

            cartContextMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InstanceNotUniqueException));
            Assert.AreEqual("This User already have a Cart", exception.Message);
        }

        [TestMethod]
        public void TestGetCart()
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

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart> { cart });
            _cartRepository = new CartRepository(cartContextMock.Object);

            var result = _cartRepository.GetCart((Cart c) => c.User.Email == cart.User.Email);

            cartContextMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(cart, result);
        }

        [TestMethod]
        public void TestGetCartInvalid()
        {
            string fakeEmail = "fake@mail.com";

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart>());
            _cartRepository = new CartRepository(cartContextMock.Object);

            Exception exception = null;

            try
            {
                _cartRepository.GetCart((Cart c) => c.User.Email == fakeEmail);
            }
            catch (Exception e)
            {
                exception = e;
            }

            cartContextMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("Cart not found", exception.Message);
        }

        [TestMethod]
        public void TestModifyCartOk()
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

            var cartModified = new Cart()
            {
                Id = cart.Id,
                User = cart.User,
                Products = cart.Products,
                Promotion = new PromoFidelity()
            };

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart> { cart });
            _cartRepository = new CartRepository(cartContextMock.Object);

            var result = _cartRepository.ModifyCart(cartModified);

            cartContextMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(cartModified.Promotion, result.Promotion);
        }

        [TestMethod]
        public void TestModifyCartInvalid()
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

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart>());
            _cartRepository = new CartRepository(cartContextMock.Object);

            Exception exception = null;

            try
            {
                _cartRepository.ModifyCart(cart);
            }
            catch (Exception e)
            {
                exception = e;
            }

            cartContextMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("Cart not found", exception.Message);
        }

        [TestMethod]
        public void TestDeleteCartOk()
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

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(c => c.RemoveCart(cart)).Returns(true);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart> { cart });
            _cartRepository = new CartRepository(cartContextMock.Object);

            var result = _cartRepository.DeleteCartById(cart.Id);

            cartContextMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestDeleteCartByIdInvalid()
        {
            var invalidCart = Guid.NewGuid();

            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(context => context.GetAllCarts()).Returns(new List<Cart>());
            _cartRepository = new CartRepository(cartContextMock.Object);

            Exception exception = null;

            try
            {
                _cartRepository.DeleteCartById(invalidCart);
            }
            catch (Exception e)
            {
                exception = e;
            }

            cartContextMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("Cart not found", exception.Message);
        }

        [TestMethod]
        public void TestUnhandledDataAccessExceptionThrown()
        {
            Mock<IMemoryContext> cartContextMock = new Mock<IMemoryContext>(MockBehavior.Strict);
            cartContextMock.Setup(context => context.GetAllCarts()).Throws(new Exception());
            _cartRepository = new CartRepository(cartContextMock.Object);
            Exception exception = null;

            try
            {
                var result = _cartRepository.GetCart((Cart c) => c.Id == Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            cartContextMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledDataAccessException));
            Assert.IsTrue(exception.Message.Equals("A DataAccess error has ocurred: " + exception.InnerException.Message));
        }
    }
}
