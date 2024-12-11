using Domain.Interfaces;
using Domain.Models;
using Domain.WebModels.In;
using Domain.WebModels.Out;
using ECommerceAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Promo20OffDlls.Models;

namespace Tests.APITests
{
    [TestClass]
    public class CartsControllerTest
    {
        private CartsController _cartsController;

        [TestMethod]
        public void TestGetCartByIdOk()
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

            Mock<ICartService> cartLogicMock = new Mock<ICartService>(MockBehavior.Strict);
            cartLogicMock.Setup(logic => logic.GetCartById(It.IsAny<Guid>())).Returns(cart);
            _cartsController = new CartsController(cartLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new CartOut(cart));
            CartOut expectedObject = expected.Value as CartOut;

            OkObjectResult result = _cartsController.GetCartById(cart.Id) as OkObjectResult;
            CartOut objectResult = result.Value as CartOut;

            cartLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestGetCartByUserIdOk()
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

            Mock<ICartService> cartLogicMock = new Mock<ICartService>(MockBehavior.Strict);
            cartLogicMock.Setup(logic => logic.GetCartByUserId(It.IsAny<Guid>())).Returns(cart);
            _cartsController = new CartsController(cartLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new CartOut(cart));
            CartOut expectedObject = expected.Value as CartOut;

            OkObjectResult result = _cartsController.GetCartByUserId(cart.Id) as OkObjectResult;
            CartOut objectResult = result.Value as CartOut;

            cartLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestAddCartCreatedOk()
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
                           Category = "Bebidas gaseosas",
                           IsPromotional = true
                       },
                       Quantity = 1
                   }
                }
            };

            var cart = cartIn.ToEntity();

            Mock<ICartService> cartLogicMock = new Mock<ICartService>(MockBehavior.Strict);
            cartLogicMock.Setup(logic => logic.AddCart(It.IsAny<Cart>())).Returns(cart);
            _cartsController = new CartsController(cartLogicMock.Object);

            CreatedAtActionResult expected = new CreatedAtActionResult(nameof(CartsController.AddCart), nameof(CartsController), null, new CartOut(cart));
            CartOut expectedObject = expected.Value as CartOut;

            CreatedAtActionResult result = _cartsController.AddCart(cartIn) as CreatedAtActionResult;
            CartOut objectResult = result.Value as CartOut;

            cartLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestModifyCartOk()
        {
            CartIn cartInModified = new CartIn
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

            Cart cartModified = cartInModified.ToEntity();

            Mock<ICartService> cartLogicMock = new Mock<ICartService>(MockBehavior.Strict);
            cartLogicMock.Setup(logic => logic.ModifyCart(It.IsAny<Cart>())).Returns(cartModified);
            _cartsController = new CartsController(cartLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new CartOut(cartModified));
            CartOut expectedObject = expected.Value as CartOut;

            OkObjectResult result = _cartsController.ModifyCart(cartModified.Id, cartInModified) as OkObjectResult;
            CartOut objectResult = result.Value as CartOut;

            cartLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestDeleteCartByIdOk()
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

            Mock<ICartService> cartLogicMock = new Mock<ICartService>(MockBehavior.Strict);

            cartLogicMock.Setup(logic => logic.DeleteCartById(It.IsAny<Guid>())).Returns(true);

            _cartsController = new CartsController(cartLogicMock.Object);

            OkObjectResult expected = new OkObjectResult("Cart Deleted successfully");
            var expectedObject = expected.Value;

            OkObjectResult result = _cartsController.DeleteCartById(cart.Id) as OkObjectResult;
            var objectResult = result.Value;

            cartLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestAddProductToCartOk()
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
                   },
                   new CartProductIn
                   {
                          Product = new Product
                          {
                            Name = "Pants2",
                            Description = "Cool Pants2",
                            Price = 2020,
                            Brand = "Levis2",
                            Colors= new List<string>{"red","blue"},
                            Category = "Bebidas gaseosas"
                          },
                          Quantity = 1
                     }
                }
            };

            var cart = cartIn.ToEntity();
            var newProd = new Product
            {
                Name = "Pants2",
                Description = "Cool Pants2",
                Price = 2020,
                Brand = "Levis2",
                Colors = new List<string> { "red", "blue" },
                Category = "Bebidas gaseosas"
            };

            Mock<ICartService> cartLogicMock = new Mock<ICartService>(MockBehavior.Strict);
            cartLogicMock.Setup(logic => logic.AddProductToCart(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(cart);
            _cartsController = new CartsController(cartLogicMock.Object);

            CreatedAtActionResult expected = new CreatedAtActionResult(nameof(CartsController.AddProductToCart), nameof(CartsController), null, new CartOut(cart));
            CartOut expectedObject = expected.Value as CartOut;

            CreatedAtActionResult result = _cartsController.AddProductToCart(cart.Id, newProd) as CreatedAtActionResult;
            CartOut objectResult = result.Value as CartOut;

            cartLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }
    }
}
