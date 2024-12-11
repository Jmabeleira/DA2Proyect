using Domain.Interfaces;
using Domain.Models;
using Logic.Exceptions;
using Logic.Services;
using Moq;

namespace Tests.LogicTests
{
    [TestClass]
    public class AuthServiceTest
    {
        private IAuthService _authService;
        private IUserService _userService;

        [TestMethod]
        public void TestGetCurrentUserValid()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            authManagerMock.Setup(x => x.GetCurrentUser()).Returns(userResponse);
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            User expectedUser = userResponse;
            User userResult = _authService.GetCurrentUser();

            authManagerMock.VerifyAll();
            Assert.AreEqual(expectedUser, userResult);
        }

        [TestMethod]
        public void TestGetCurrentUserInvalid()
        {
            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            authManagerMock.Setup(x => x.GetCurrentUser()).Throws(new CurrentUserException("No user logged in", It.IsAny<Exception>()));
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            Exception ex = null;

            try
            {
                _authService.GetCurrentUser();
            }
            catch (Exception e)
            {
                ex = e;
            }

            authManagerMock.VerifyAll();
            Assert.IsInstanceOfType(ex, typeof(CurrentUserException));
            Assert.AreEqual("No user logged in", ex.Message);
        }

        [TestMethod]
        public void TestLoginValid()
        {
            var userLogin = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            authManagerMock.Setup(x => x.Login(userLogin)).Returns(userLogin);
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            User expectedUser = userLogin;
            User userResult = _authService.Login(userLogin);

            authManagerMock.VerifyAll();
            Assert.AreEqual(expectedUser, userResult);
        }

        [TestMethod]
        public void TestLoginInvalid()
        {
            var userLogin = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            authManagerMock.Setup(x => x.Login(userLogin)).Throws(new InvalidCredentialsException("Invalid credentials", It.IsAny<Exception>()));
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            Exception ex = null;

            try
            {
                _authService.Login(userLogin);
            }
            catch (Exception e)
            {
                ex = e;
            }

            authManagerMock.VerifyAll();

            Assert.IsInstanceOfType(ex, typeof(InvalidCredentialsException));
            Assert.AreEqual("Invalid credentials", ex.Message);
        }

        [TestMethod]
        public void TestLogoutValid()
        {
            var userLogout = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            authManagerMock.Setup(x => x.Logout()).Returns(userLogout);
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            User expectedUser = userLogout;
            User userResult = _authService.Logout();

            authManagerMock.VerifyAll();

            Assert.AreEqual(expectedUser, userResult);
        }

        [TestMethod]
        public void TestLogoutInvalid()
        {
            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            authManagerMock.Setup(x => x.Logout()).Throws(new CurrentUserException("No user logged in", It.IsAny<Exception>()));
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            Exception ex = null;

            try
            {
                _authService.Logout();
            }
            catch (Exception e)
            {
                ex = e;
            }

            authManagerMock.VerifyAll();

            Assert.IsInstanceOfType(ex, typeof(CurrentUserException));
            Assert.AreEqual("No user logged in", ex.Message);
        }

        [TestMethod]
        public void TestRegisterValid()
        {
            var userLogin = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);

            userServiceMock.Setup(x => x.AddUser(userLogin)).Returns(userLogin);
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            User expectedUser = userLogin;
            User userResult = _authService.RegisterUser(userLogin);

            userServiceMock.VerifyAll();
            Assert.AreEqual(expectedUser, userResult);
        }

        [TestMethod]
        public void TestRegisterUserInvalid()
        {
            var userLogin = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IAuthManager> authManagerMock = new Mock<IAuthManager>(MockBehavior.Strict);
            Mock<IUserService> userServiceMock = new Mock<IUserService>(MockBehavior.Strict);

            userServiceMock.Setup(x => x.AddUser(userLogin)).Throws(new AlreadyExistsException("User already exists", It.IsAny<Exception>()));
            _authService = new AuthService(authManagerMock.Object, userServiceMock.Object);

            Exception ex = null;

            try
            {
                _authService.RegisterUser(userLogin);
            }
            catch (Exception e)
            {
                ex = e;
            }

            userServiceMock.VerifyAll();

            Assert.IsInstanceOfType(ex, typeof(AlreadyExistsException));
            Assert.AreEqual("User already exists", ex.Message);
        }
    }
}
