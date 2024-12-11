using Domain.Models;
using PromoTotalLookDlls.Models;

namespace Tests.DomainTests.PromoTests
{
    [TestClass]
    public class PromoTotalLookTest
    {
        private Promotion promoTotalLook;
        private List<CartProduct> cartProducts;
        private CartProduct cartProduct1;
        private CartProduct cartProduct2;
        private CartProduct cartProduct3;
        private CartProduct cartProduct4;
        private CartProduct cartProduct5;

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
                Name = "Product2",
                Description = "Product2",
                Price = 2000,
                Category = "Category2",
                Brand = "Brand2",
                Colors = new List<string> { "red", "blue" }

            };
            cartProduct4.Quantity = 1;
            cartProduct5 = new CartProduct();
            cartProduct5.Product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product3",
                Description = "Product3",
                Price = 300,
                Category = "Category2",
                Brand = "Brand2",
                Colors = new List<string> { "red", "blue" }

            };
            cartProduct5.Quantity = 1;
            cartProducts.Add(cartProduct4);
            cartProducts.Add(cartProduct5);
            promoTotalLook = new PromoTotalLook();
        }

        [TestMethod]
        public void PromoIsApplicableTest()
        {
            Assert.IsTrue(promoTotalLook.IsApplicable(cartProducts));
        }

        [TestMethod]
        public void PromoIsNotApplicableTest()
        {
            cartProducts.Remove(cartProduct3);
            cartProducts.Remove(cartProduct1);
            cartProducts.Remove(cartProduct2);
            cartProducts.Remove(cartProduct4);
            Assert.IsFalse(promoTotalLook.IsApplicable(cartProducts));
        }

        [TestMethod]
        public void PromoCalculateDiscountTest()
        {
            Assert.AreEqual(1000, promoTotalLook.CalculateDiscount(cartProducts));
        }
    }
}
