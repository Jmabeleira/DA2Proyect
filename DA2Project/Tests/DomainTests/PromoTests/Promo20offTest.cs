using Domain.Models;
using Promo20OffDlls.Models;

namespace Tests.DomainTests.PromoTests
{
    [TestClass]
    public class Promo20offTest
    {
        private Promotion promo20off;
        private List<CartProduct> cartProducts;
        private CartProduct cartProduct1;
        private CartProduct cartProduct2;

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
            cartProduct1.Quantity = 1;
            cartProduct2 = new CartProduct();
            cartProduct2.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product2",
                Description = "Product2",
                Price = 200,
                Category = "Category2",
                Brand = "Brand2",
                Colors = new List<string> { "red", "blue" }

            };
            cartProducts.Add(cartProduct1);
            cartProducts.Add(cartProduct2);
            promo20off = new Promo20Off();
        }

        [TestMethod]
        public void PromoIsApplicableTest()
        {
            bool result = promo20off.IsApplicable(cartProducts);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PromoIsNotApplicableTest()
        {
            cartProducts.Remove(cartProduct2);

            bool result = promo20off.IsApplicable(cartProducts);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PromoCalculateDiscountTest()
        {
            double expected = 40;

            double result = promo20off.CalculateDiscount(cartProducts);

            Assert.AreEqual(expected, result);
        }
    }
}
