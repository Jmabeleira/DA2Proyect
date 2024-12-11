using Domain.Models;
using Promo3X2Dlls.Models;

namespace Tests.DomainTests.PromoTests
{
    [TestClass]
    public class Promo3x2Test
    {
        private Promotion promo3x2;
        private List<CartProduct> cartProducts;
        private CartProduct cartProduct1;
        private CartProduct cartProduct2;
        private CartProduct cartProduct3;
        private CartProduct cartProduct4;
        private CartProduct cartProduct5;
        private CartProduct cartProduct6;

        [TestInitialize]
        public void TestInitialize()
        {
            cartProducts = new List<CartProduct>();
            cartProduct1 = new CartProduct();
            cartProduct1.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product1",
                Description = "Product1",
                Price = 100,
                Category = "Category1",
                Brand = "Brand1",
                Colors = new List<string> { "red", "green" }

            };
            cartProduct1.Quantity = 2;
            cartProduct2 = new CartProduct();
            cartProduct2.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product2",
                Description = "Product2",
                Price = 200,
                Category = "Category1",
                Brand = "Brand1",
                Colors = new List<string> { "red", "blue" }

            };
            cartProduct2.Quantity = 1;
            cartProduct3 = new CartProduct();
            cartProduct3.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product3",
                Description = "Product3",
                Price = 300,
                Category = "Category1",
                Brand = "Brand1",
                Colors = new List<string> { "red", "blue" }

            };
            cartProduct3.Quantity = 1;
            cartProducts.Add(cartProduct1);
            cartProducts.Add(cartProduct2);
            cartProducts.Add(cartProduct3);
            cartProduct4 = new CartProduct();
            cartProduct4.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product1",
                Description = "Product1",
                Price = 1000,
                Category = "Category2",
                Brand = "Brand2",
                Colors = new List<string> { "red", "green" }

            };
            cartProduct4.Quantity = 1;
            cartProduct5 = new CartProduct();
            cartProduct5.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product2",
                Description = "Product2",
                Price = 2000,
                Category = "Category2",
                Brand = "Brand2",
                Colors = new List<string> { "red", "blue" }

            };
            cartProduct5.Quantity = 1;
            cartProduct6 = new CartProduct();
            cartProduct6.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product3",
                Description = "Product3",
                Price = 300,
                Category = "Category2",
                Brand = "Brand2",
                Colors = new List<string> { "red", "blue" }

            };
            cartProduct6.Quantity = 1;
            cartProducts.Add(cartProduct4);
            cartProducts.Add(cartProduct5);
            cartProducts.Add(cartProduct6);
            promo3x2 = new Promo3X2();
        }

        [TestMethod]
        public void CalculateDiscountTest()
        {
            var result = promo3x2.CalculateDiscount(cartProducts);
            Assert.AreEqual(300, result);
        }

        [TestMethod]
        public void IsApplicableTest()
        {
            var result = promo3x2.IsApplicable(cartProducts);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotApplicableTest()
        {
            cartProducts.Remove(cartProduct1);
            cartProducts.Remove(cartProduct2);
            cartProducts.Remove(cartProduct4);
            var result = promo3x2.IsApplicable(cartProducts);
            Assert.IsFalse(result);
        }
    }
}
