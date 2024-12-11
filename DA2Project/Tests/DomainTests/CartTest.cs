using Domain.Exceptions;
using Domain.Models;
using Domain.WebModels.Out;

namespace Tests.DomainTests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void TestAddProduct()
        {

            var cart = new Cart();
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };

            cart.AddProduct(product);

            Assert.IsTrue(cart.Products.Any(cp => cp.Product == product));
        }

        [TestMethod]
        public void TestRemoveProduct()
        {
            var cart = new Cart();
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };

            cart.AddProduct(product);
            cart.RemoveProduct(product);

            Assert.ThrowsException<ProductNotFoundException>(() => cart.GetProduct(product.Id));
        }

        [TestMethod]
        public void TestRemoveAllProducts()
        {
            var cart = new Cart();
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };

            cart.AddProduct(product);
            cart.RemoveAllProducts();

            Assert.ThrowsException<ProductNotFoundException>(() => cart.GetProduct(product.Id));
        }

        [TestMethod]
        public void TestAddProducts()
        {
            var cart = new Cart();
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Product",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                },
                new Product
                {
                    Name = "Product2",
                    Price = 100,
                    Description = "Description",
                    Brand = "Brand",
                    Colors = new List<string> { "Red", "Blue" },
                    Category = "Category"
                }
            };

            cart.AddProducts(products);

            Assert.IsTrue(cart.Products.Count == 2);
        }

        [TestMethod]
        public void TestRemoveCartThrowsException()
        {
            var cart = new Cart();
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };

            Assert.ThrowsException<ProductNotFoundException>(() => cart.RemoveProduct(product));
        }

        [TestMethod]
        public void TestCartProductHasTheSameProductTwice()
        {
            var cart = new Cart();
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };

            cart.AddProduct(product);
            cart.AddProduct(product);

            Assert.IsTrue(cart.Products.Any(cp => cp.Product == product && cp.Quantity == 2));
        }

        [TestMethod]
        public void TestGetProductThrowsException()
        {
            var cart = new Cart();
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };

            Assert.ThrowsException<ProductNotFoundException>(() => cart.GetProduct(product.Id));
        }

        [TestMethod]
        public void TestCartOutEqualsWithNull()
        {
            var user = new User
            {
                Email = "eren@gmail.com",
                UserRole = new List<UserRole> { UserRole.Client },
                Password = "piripitiflauticoMagico",
                Address = new Address { Country = "Colombia", City = "Bogota", Street = "Calle 123", ZipCode = "123-45", DoorNumber = 201 }
            };
            var cart = new Cart();
            var product = new Product
            {
                Name = "Product",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Colors = new List<string> { "Red", "Blue" },
                Category = "Category"
            };
            var cartProduct = new CartProduct { Id = Guid.NewGuid(), Product = product, Quantity = 1 };
            cart.Products.Add(cartProduct);
            cart.User = user;

            var cartOut = new CartOut(cart);

            Assert.IsFalse(cartOut.Equals(cart));
        }
    }
}
