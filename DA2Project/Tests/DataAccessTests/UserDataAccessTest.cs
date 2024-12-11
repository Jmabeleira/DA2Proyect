using DataAccess.Exceptions;
using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Dtos.Interfaces;
using Dtos.Mappers;
using Dtos.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace Tests.DataAccessTests
{
    [TestClass]
    public class UserDataAccessTest
    {
        IUserRepository _userRepository;

        [TestMethod]
        public void TestAddUserValid()
        {
            var user = new User
            {
                Email = "eren120@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { });
            userContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _userRepository = new UserRepository(userContextMock.Object);

            var result = _userRepository.AddUser(user);

            userContextMock.Verify(x => x.Users);
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void TestAddUserInvalid()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            _userRepository = new UserRepository(userContextMock.Object);

            Exception exception = null;

            try
            {
                _userRepository.AddUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            userContextMock.Verify(x => x.Users);
            Assert.IsInstanceOfType(exception, typeof(InstanceNotUniqueException));
            Assert.AreEqual("User is not unique", exception.Message);
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            _userRepository = new UserRepository(userContextMock.Object);


            var result = _userRepository.GetAllUsers();

            userContextMock.Verify(x => x.Users);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.First(), user);
        }

        [TestMethod]
        public void TestGetAllUsersInvalid()
        {
            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { });
            _userRepository = new UserRepository(userContextMock.Object);

            Exception exception = null;

            try
            {
                _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userContextMock.Verify(x => x.Users);
            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.IsTrue(exception.Message.Equals("There are no Users"));
        }

        [TestMethod]
        public void TestDeleteUserOk()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            userContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _userRepository = new UserRepository(userContextMock.Object);

            var result = _userRepository.DeleteUserById(user.Id);

            userContextMock.Verify(x => x.Users);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void TestDeleteUserInvalid()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            _userRepository = new UserRepository(userContextMock.Object);

            Exception exception = null;

            try
            {
                _userRepository.DeleteUserById(Guid.NewGuid());
            }
            catch (Exception e)
            {
                exception = e;
            }

            userContextMock.Verify(x => x.Users);
            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("User not found", exception.Message);
        }

        [TestMethod]
        public void TestGetUserOk()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            _userRepository = new UserRepository(userContextMock.Object);

            var result = _userRepository.GetUser((User u) => u.Id == user.Id);

            userContextMock.Verify(x => x.Users);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, user);
        }

        [TestMethod]
        public void TestGetUserInvalid()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            _userRepository = new UserRepository(userContextMock.Object);

            Exception exception = null;

            try
            {
                _userRepository.GetUser((User u) => u.Id == Guid.NewGuid());
            }
            catch (Exception e)
            {
                exception = e;
            }

            userContextMock.Verify(x => x.Users);
            Assert.IsInstanceOfType(exception, typeof(NoInstanceException));
            Assert.AreEqual("User not found", exception.Message);
        }

        [TestMethod]
        public void TestModifyUserOk()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            userContextMock.Setup(x => x.Addresses).ReturnsDbSet(new List<AddressDto> { AddressHelper.FromAddressToAddressDto(user.Address) });
            userContextMock.Setup(x => x.SaveChanges()).Returns(1);
            _userRepository = new UserRepository(userContextMock.Object);

            user.Email = "erenModified@gmail.com";
            var result = _userRepository.ModifyUser(user);

            userContextMock.Verify(x => x.Users);
            userContextMock.Verify(x => x.Addresses);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, user);
        }

        [TestMethod]
        public void TestModifyUserInvalid()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var invalidUser = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            var addresses = new List<AddressDto> { AddressHelper.FromAddressToAddressDto(new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }) };

            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).ReturnsDbSet(new List<UserDto> { UserHelper.FromUserToUserDto(user) });
            _userRepository = new UserRepository(userContextMock.Object);

            Exception exception = null;

            try
            {
                _userRepository.ModifyUser(invalidUser);
            }
            catch (Exception e)
            {
                exception = e;
            }

            userContextMock.Verify(x => x.Users);
            Assert.IsInstanceOfType(exception, typeof(UnhandledDataAccessException));
            Assert.AreEqual("A DataAccess error has ocurred: User not found", exception.Message);
        }

        [TestMethod]
        public void TestUnhandledExceptionThrown()
        {
            var userContextMock = new Mock<IContextDb>(MockBehavior.Strict);
            userContextMock.Setup(x => x.Users).Throws(new Exception());
            _userRepository = new UserRepository(userContextMock.Object);

            Exception exception = null;

            try
            {
                _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userContextMock.Verify(x => x.Users);
            Assert.IsInstanceOfType(exception, typeof(UnhandledDataAccessException));
            Assert.IsTrue(exception.Message.Equals("A DataAccess error has ocurred: " + exception.InnerException.Message));
        }
    }
}
