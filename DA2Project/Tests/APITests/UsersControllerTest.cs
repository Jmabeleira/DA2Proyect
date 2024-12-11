using ECommerceAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Interfaces;
using Moq;
using Domain.WebModels.In;
using Domain.WebModels.Out;
using Microsoft.AspNetCore.Http;
using Domain.Exceptions;

namespace Tests.APITests
{
    [TestClass]
    public class UsersControllerTest
    {
        private UsersController _usersController;

        [TestMethod]
        public void TestAddUser()
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

            AddUserIn userIn = new AddUserIn
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new AddressIn
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            User user = userIn.ToEntity();
            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(userAuth);
            userLogicMock.Setup(logic => logic.AddUser(It.IsAny<User>())).Returns(user);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            CreatedAtActionResult expected = new CreatedAtActionResult(nameof(UsersController.AddUser), nameof(UsersController), null, new UserOut(user));
            UserOut expectedObject = expected.Value as UserOut;

            CreatedAtActionResult result = _usersController.AddUser(userIn) as CreatedAtActionResult;
            UserOut objectResult = result.Value as UserOut;

            userLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestAddUserUnauthorized()
        {
            AddUserIn userIn = new AddUserIn
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new AddressIn
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            User user = userIn.ToEntity();
            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(user);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _usersController.AddUser(userIn);
            }
            catch (Exception e)
            {
                ex = e;
            }

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void GetAllUsersOk()
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

            User user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(userAuth);
            userLogicMock.Setup(logic => logic.GetAllUsers()).Returns(new List<User> { user });

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<UserOut> { new UserOut(user) });
            List<UserOut> expectedObject = expected.Value as List<UserOut>;

            OkObjectResult result = _usersController.GetAllUsers() as OkObjectResult;
            List<UserOut> objectResult = result.Value as List<UserOut>;

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.IsTrue(expectedObject.SequenceEqual(objectResult));
        }

        [TestMethod]
        public void GetAllUsersUnauthorized()
        {
            User user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(user);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _usersController.GetAllUsers();
            }
            catch (Exception e)
            {
                ex = e;
            }

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void GetUserByIdOk()
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
            User user = new User
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
            };

            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(userAuth);
            userLogicMock.Setup(logic => logic.GetUserById(It.IsAny<Guid>())).Returns(user);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new UserOut(user));
            UserOut expectedObject = expected.Value as UserOut;

            OkObjectResult result = _usersController.GetUserById(user.Id) as OkObjectResult;
            UserOut objectResult = result.Value as UserOut;

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode)
                                                         && expectedObject.Email.Equals(objectResult.Email));
        }

        [TestMethod]
        public void GetUserByIdUnauthorized()
        {
            User unauthorizedUser = new User
            {
                Email = "eren2@gmail.com",
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
            User user = new User
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
            };

            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(unauthorizedUser);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _usersController.GetUserById(user.Id);
            }
            catch (Exception e)
            {
                ex = e;
            }

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void ModifyUserOk()
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
            UserIn userInModified = new UserIn
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new AddressIn
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            User userModified = userInModified.ToEntity();

            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            userLogicMock.Setup(logic => logic.ModifyUser(It.IsAny<User>())).Returns(userModified);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(userAuth);
            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new UserOut(userModified));
            UserOut expectedObject = expected.Value as UserOut;

            OkObjectResult result = _usersController.ModifyUserById(userModified.Id, userInModified) as OkObjectResult;
            UserOut objectResult = result.Value as UserOut;

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void ModifyUserUnauthorized()
        {
            User unauthorizedUser = new User
            {
                Email = "eren2@gmail.com",
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
            UserIn userInModified = new UserIn
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new AddressIn
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(unauthorizedUser);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _usersController.ModifyUserById(Guid.NewGuid(), userInModified);
            }
            catch (Exception e)
            {
                ex = e;
            }

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void DeleteUserByIdOk()
        {
            User userAuth = new User
            {
                Email = "eren2@gmail.com",
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
            UserIn userIn = new UserIn
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new AddressIn
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            var user = userIn.ToEntity();

            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(userAuth);
            userLogicMock.Setup(logic => logic.DeleteUserById(It.IsAny<Guid>())).Returns(true);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult("User deleted successfully");
            bool expectedObject = expected.Value != null ? true : false;

            OkObjectResult result = _usersController.DeleteUserById(user.Id) as OkObjectResult;
            bool objectResult = result.Value != null ? true : false;

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void DeleteUserByIdUnauthorized()
        {
            User unauthorizedUser = new User
            {
                Email = "eren2@gmail.com",
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
            UserIn userIn = new UserIn
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new AddressIn
                {
                    Country = "Colombia",
                    City = "Bogota",
                    Street = "Calle 123",
                    ZipCode = "123-45",
                    DoorNumber = 201
                }
            };

            Mock<IUserService> userLogicMock = new Mock<IUserService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(unauthorizedUser);

            _usersController = new UsersController(userLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _usersController.DeleteUserById(Guid.NewGuid());
            }
            catch (Exception e)
            {
                ex = e;
            }

            userLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }
    }
}
