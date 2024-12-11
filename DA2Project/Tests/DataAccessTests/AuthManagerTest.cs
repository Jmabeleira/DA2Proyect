using DataAccess.Managers;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Tests.DataAccessTests
{
    [TestClass]
    public class AuthManagerTest
    {
        [TestMethod]
        public void TestGetCurrentUserOk()
        {
            User user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepoMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Returns(user);
            AuthManager authManager = new AuthManager(userRepoMock.Object);

            var userLoggedIn = authManager.Login(user);
            var currentUser = authManager.GetCurrentUser();

            userRepoMock.VerifyAll();
            Assert.AreEqual(userLoggedIn, currentUser);
        }

        [TestMethod]
        public void TestGetCurrentUserInvalid()
        {
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            AuthManager authManager = new AuthManager(userRepoMock.Object);

            Exception ex = null;

            authManager.Logout();

            try
            {
                var currentUser = authManager.GetCurrentUser();
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.IsNotNull(ex);
            Assert.AreEqual(ex.Message, "No user logged in");
        }

        [TestMethod]
        public void TestLoginOk()
        {
            User user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepoMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Returns(user);
            AuthManager authManager = new AuthManager(userRepoMock.Object);

            var expectedUser = user;
            var userLoggedIn = authManager.Login(user);

            Assert.IsNotNull(userLoggedIn);
            Assert.AreEqual(userLoggedIn, user);
        }

        [TestMethod]
        public void TestLoginInvalid()
        {
            User user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepoMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Returns((Func<User, bool> predicate) => null);
            AuthManager authManager = new AuthManager(userRepoMock.Object);

            Exception ex = null;

            try
            {
                var userLoggedIn = authManager.Login(user);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.IsNotNull(ex);
            Assert.AreEqual(ex.Message, "Invalid credentials");
        }

        [TestMethod]
        public void TestLogoutOk()
        {
            User user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepoMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Returns(user);
            AuthManager authManager = new AuthManager(userRepoMock.Object);

            authManager.Login(user);
            var userLoggedOut = authManager.Logout();

            userRepoMock.VerifyAll();
            Assert.AreEqual(user, userLoggedOut);
        }
    }
}
