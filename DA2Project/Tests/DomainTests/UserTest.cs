using Domain.Exceptions;
using Domain.Models;
using Domain.WebModels.Out;

namespace Tests.DomainTests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestUserCreation()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestUserEmailIsNotValid()
        {
            string email = "";
            User user = new User();

            Exception exception = null;

            try
            {
                user.Email = email;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestValidationException));
            Assert.IsTrue(exception.Message.Equals("The provided email address is not valid"));
        }

        [TestMethod]
        public void TestUserEmailIsValid()
        {
            string email = "eren@gmail.com";

            User user = new User
            {
                Email = email,
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };

            Assert.AreEqual(email, user.Email);
        }

        [TestMethod]
        public void TestUserAddressIsValid()
        {
            Address address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 };
            User user = new User();

            user.Address = address;

            Assert.AreEqual(address, user.Address);
        }

        [TestMethod]
        public void TestUserAddressIsNotValid()
        {

            string country = string.Empty;
            string city = string.Empty;
            string street = string.Empty;
            string zipCode = string.Empty;
            int doorNumber = -1;

            Assert.ThrowsException<RequestValidationException>(() => new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
                DoorNumber = doorNumber
            });
        }

        [TestMethod]
        public void TestCountryInvalid()
        {
            string country = string.Empty;
            string city = "City";
            string street = "Street";
            string zipCode = "11000";
            int doorNumber = 2345;

            Assert.ThrowsException<RequestValidationException>(() => new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
                DoorNumber = doorNumber
            });
        }

        [TestMethod]
        public void TestCityInvalid()
        {
            string country = "Country";
            string city = string.Empty;
            string street = "Street";
            string zipCode = "11000";
            int doorNumber = 2345;

            Assert.ThrowsException<RequestValidationException>(() => new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
                DoorNumber = doorNumber
            });
        }

        [TestMethod]
        public void TestStreetInvalid()
        {
            string country = "Country";
            string city = "City";
            string street = string.Empty;
            string zipCode = "11000";
            int doorNumber = 2345;

            Assert.ThrowsException<RequestValidationException>(() => new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
                DoorNumber = doorNumber
            });
        }

        [TestMethod]
        public void TestZipCodeInvalid()
        {
            string country = "Country";
            string city = "City";
            string street = "Street";
            string zipCode = string.Empty;
            int doorNumber = 2345;

            Assert.ThrowsException<RequestValidationException>(() => new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
                DoorNumber = doorNumber
            });
        }

        [TestMethod]
        public void TestDoorNumberInvalid()
        {
            string country = "Country";
            string city = "City";
            string street = "Street";
            string zipCode = "11000";
            int doorNumber = -1;

            Assert.ThrowsException<RequestValidationException>(() => new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
                DoorNumber = doorNumber
            });
        }

        [TestMethod]
        public void TestDoorAddressOutWithNull()
        {
            Assert.IsNotNull(new AddressOut(null));
        }

        [TestMethod]
        public void TestDoorAddressOutEqualsWithNull()
        {
            string country = "Country";
            string city = "City";
            string street = "Street";
            string zipCode = "11000";
            int doorNumber = 123;

            var address = new Address
            {
                Country = country,
                City = city,
                Street = street,
                ZipCode = zipCode,
                DoorNumber = doorNumber
            };

            var addressOut = new AddressOut(address);


            Assert.IsFalse(addressOut.Equals(address));
        }
    }
}