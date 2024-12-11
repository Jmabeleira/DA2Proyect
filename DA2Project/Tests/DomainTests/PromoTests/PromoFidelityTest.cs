using Domain.Models;
using PromoFidelityDlls.Models;

namespace Tests.DomainTests.PromoTests
{
    [TestClass]
    public class PromoFidelityTest
    {
        private Promotion promoFidelity;
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
                Category = "Category2",
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
                Category = "Category3",
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
                Category = "Category1",
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
                Category = "Category3",
                Brand = "Brand2",
                Colors = new List<string> { "red", "blue" }

            };
            cartProduct6.Quantity = 1;
            cartProducts.Add(cartProduct4);
            cartProducts.Add(cartProduct5);
            cartProducts.Add(cartProduct6);
            promoFidelity = new PromoFidelity();
        }

        [TestMethod]
        public void PromoIsApplicableTest()
        {
            bool result = promoFidelity.IsApplicable(cartProducts);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PromoIsNotApplicableTest()
        {
            cartProducts.Remove(cartProduct3);
            cartProducts.Remove(cartProduct2);
            cartProducts.Remove(cartProduct4);

            bool result = promoFidelity.IsApplicable(cartProducts);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PromoCalculateDiscountTest()
        {
            double result = promoFidelity.CalculateDiscount(cartProducts);

            Assert.AreEqual(1300, result);
        }
    }
}
