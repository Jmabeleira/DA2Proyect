using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.WebModels.In;
using Domain.WebModels.Out;
using ECommerceAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.APITests
{
    [TestClass]
    public class AuthControllerTest
    {
        private AuthsController _authsController;

        [TestMethod]
        public void TestLoginOk()
        {
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
            var userLogIn = new UserLogin { Password = user.Password, Email = user.Email };

            Mock<IAuthService> authLogicMock = new Mock<IAuthService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            authLogicMock.Setup(logic => logic.Login(It.IsAny<User>())).Returns(user);
            _authsController = new AuthsController(authLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new UserOut(user));
            UserOut expectedObject = expected.Value as UserOut;

            OkObjectResult result = _authsController.Login(userLogIn) as OkObjectResult;
            UserOut objectResult = result.Value as UserOut;

            authLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestLogOutOk()
        {
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

            Mock<IAuthService> authLogicMock = new Mock<IAuthService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            authLogicMock.Setup(logic => logic.Logout()).Returns(user);
            _authsController = new AuthsController(authLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new UserOut(user));
            UserOut expectedObject = expected.Value as UserOut;

            OkObjectResult result = _authsController.LogOut() as OkObjectResult;
            UserOut objectResult = result.Value as UserOut;

            authLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void TestGetCurrentUserOk()
        {
            User authenticatedUser = new User
            {
                Email = "eren@gmail.com",
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

            Mock<IAuthService> authLogicMock = new Mock<IAuthService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(authenticatedUser);
            authLogicMock.Setup(logic => logic.GetCurrentUser()).Returns(user);

            _authsController = new AuthsController(authLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new UserOut(user));
            UserOut expectedObject = expected.Value as UserOut;

            OkObjectResult result = _authsController.GetCurrentUser() as OkObjectResult;
            UserOut objectResult = result.Value as UserOut;

            authLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void GetCurrentUserInvalid()
        {
            User unauthorizedUser = new User
            {
                Email = "eren2@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico2",
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

            Mock<IAuthService> authLogicMock = new Mock<IAuthService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContextMock = new Mock<HttpContext>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
            httpContextMock.SetupGet(x => x.Items["user"]).Returns(unauthorizedUser);

            _authsController = new AuthsController(authLogicMock.Object, httpContextAccessorMock.Object);

            Exception ex = null;

            try
            {
                _authsController.GetCurrentUser();
            }
            catch (Exception e)
            {
                ex = e;
            }

            authLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();
            httpContextMock.VerifyAll();

            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(InvalidPermissionException));
        }

        [TestMethod]
        public void TestRegisterUserOk()
        {
            UserToRegisterIn userRegister = new UserToRegisterIn
            {
                Email = "eren@gmail.com",
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

            var user = userRegister.ToEntity();

            Mock<IAuthService> authLogicMock = new Mock<IAuthService>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            authLogicMock.Setup(logic => logic.RegisterUser(It.IsAny<User>())).Returns(user);
            _authsController = new AuthsController(authLogicMock.Object, httpContextAccessorMock.Object);

            OkObjectResult expected = new OkObjectResult(new UserOut(user));
            UserOut expectedObject = expected.Value as UserOut;

            OkObjectResult result = _authsController.RegisterUser(userRegister) as OkObjectResult;
            UserOut objectResult = result.Value as UserOut;

            authLogicMock.VerifyAll();
            httpContextAccessorMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode));
            Assert.AreEqual(expectedObject, objectResult);
        }
    }
}
