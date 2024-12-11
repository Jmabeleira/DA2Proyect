using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Logic.Exceptions;
using Logic.Services;
using Moq;

namespace Tests.LogicTests
{
    [TestClass]
    public class UserServiceTest
    {
        IUserService _userService;

        [TestMethod]
        public void TestGetUserByIdValidTest()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Returns(userResponse);
            _userService = new UserService(userRepositoryMock.Object);

            User expectedUser = userResponse;
            User userResult = _userService.GetUserById(userResponse.Id);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(userResult, expectedUser);
        }

        [TestMethod]
        public void TestGetUserByIdInvalid()
        {
            var falseId = Guid.NewGuid();
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Throws(new NoInstanceException(It.IsAny<string>()));
            _userService = new UserService(userRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _userService.GetUserById(falseId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("There is no such User"));
        }

        [TestMethod]
        public void TestGetAllUsersValid()
        {
            var usersResponse = new List<User>
            {
                new User
                {
                    Email = "eren@gmail.com",
                    UserRole = new List<UserRole> { UserRole.Client },
                    Password = "piripitiflauticoMagico",
                    Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
                }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetAllUsers()).Returns(usersResponse);
            _userService = new UserService(userRepositoryMock.Object);

            List<User> expectedUsers = usersResponse;
            List<User> usersResult = _userService.GetAllUsers();

            userRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedUsers.SequenceEqual(usersResult));
        }

        [TestMethod]
        public void TestGetAllUsersInvalid()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetAllUsers()).Throws(new NoInstanceException("There are no Users"));
            _userService = new UserService(userRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _userService.GetAllUsers();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NoContentException));
            Assert.IsTrue(exception.Message.Equals("There are no Users"));
        }

        [TestMethod]
        public void TestAddUserValid()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>())).Returns(userResponse);
            _userService = new UserService(userRepositoryMock.Object);

            User expectedUser = userResponse;
            User userResult = _userService.AddUser(userResponse);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(userResult, expectedUser);
        }

        [TestMethod]
        public void TestAddUserInvalid()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>())).Throws(new InstanceNotUniqueException(It.IsAny<string>()));
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.AddUser(userResponse);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(AlreadyExistsException));
            Assert.IsTrue(exception.Message.Equals("User already exists"));
        }

        [TestMethod]
        public void TestModifyUserByIdValid()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.ModifyUser(It.IsAny<User>())).Returns(userResponse);
            _userService = new UserService(userRepositoryMock.Object);

            User expectedUser = userResponse;
            User userResult = _userService.ModifyUser(userResponse);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedUser, userResult);
        }

        [TestMethod]
        public void TestModifyUserByIdInvalid()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.ModifyUser(It.IsAny<User>())).Throws(new NoInstanceException(It.IsAny<string>()));
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.ModifyUser(userResponse);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("User does not exist"));
        }

        [TestMethod]
        public void TestDeleteUserByIdValid()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.DeleteUserById(It.IsAny<Guid>())).Returns(true);
            _userService = new UserService(userRepositoryMock.Object);

            bool expectedBoolean = true;
            bool booleanResult = _userService.DeleteUserById(userResponse.Id);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedBoolean, booleanResult);
        }

        [TestMethod]
        public void TestDeleteUserByIdInvalid()
        {
            var invalidId = Guid.NewGuid();

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.DeleteUserById(It.IsAny<Guid>())).Throws(new NoInstanceException(It.IsAny<string>())); ;
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.DeleteUserById(invalidId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("User does not exist"));
        }

        [TestMethod]
        public void TestGetUserByTokenValid()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 },
                Token = Guid.NewGuid(),
                Timestamp = DateTime.Now
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Returns(userResponse);
            _userService = new UserService(userRepositoryMock.Object);

            User expectedUser = userResponse;
            User userResult = _userService.GetUserByToken(userResponse.Token);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedUser, userResult);
        }

        [TestMethod]
        public void TestGetUserByTokenInvalid()
        {
            var invalidToken = Guid.NewGuid();

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Throws(new NoInstanceException(It.IsAny<string>())); ;
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.GetUserByToken(invalidToken);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
            Assert.IsTrue(exception.Message.Equals("User does not exist"));
        }

        [TestMethod]
        public void TestIsValidTokenValid()
        {
            var validToken = Guid.NewGuid();

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.Exist(It.IsAny<Func<User, bool>>())).Returns(true);
            _userService = new UserService(userRepositoryMock.Object);

            bool result = _userService.IsValidToken(validToken);

            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsValidTokenFails()
        {
            var invalidToken = Guid.NewGuid();

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.Exist(It.IsAny<Func<User, bool>>())).Throws(new ArgumentException("Invalid token format"));
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.IsValidToken(invalidToken);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: Invalid token format"));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetUserById()
        {
            var falseId = Guid.NewGuid();
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Throws(new Exception());
            _userService = new UserService(userRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _userService.GetUserById(falseId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetAllUsers()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetAllUsers()).Throws(new Exception());
            _userService = new UserService(userRepositoryMock.Object);
            Exception exception = null;

            try
            {
                _userService.GetAllUsers();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtAddUser()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.AddUser(It.IsAny<User>())).Throws(new Exception());
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.AddUser(userResponse);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtModifyUserById()
        {
            var userResponse = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.ModifyUser(It.IsAny<User>())).Throws(new Exception());
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.ModifyUser(userResponse);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtDeleteUserById()
        {
            var invalidId = Guid.NewGuid();

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.DeleteUserById(It.IsAny<Guid>())).Throws(new Exception()); ;
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.DeleteUserById(invalidId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }

        [TestMethod]
        public void TestUnhandledLogicExceptionThrownAtGetUserByToken()
        {
            var invalidToken = Guid.NewGuid();

            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<Func<User, bool>>())).Throws(new Exception()); ;
            _userService = new UserService(userRepositoryMock.Object);

            Exception exception = null;

            try
            {
                _userService.GetUserByToken(invalidToken);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UnhandledLogicException));
            Assert.IsTrue(exception.Message.Equals("An unexpected error has ocurred: " + exception.InnerException.Message));
        }
    }
}
