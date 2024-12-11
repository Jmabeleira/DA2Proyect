using Domain.Models;

namespace Promo3X2Dlls.Models
{
    public class Promo3X2 : Promotion
    {
        public override string Name { get; } = "Promo 3x2";
        public override string Description { get; } = "El producto de menor valor es gratis";
        public override string Condition { get; } = "Tener 3 productos de la misma categoría";

        public Promo3X2()
        {
            Id = Guid.NewGuid();
        }

        public override double CalculateDiscount(IEnumerable<CartProduct> cartProducts)
        {
            var maxDiscount = cartProducts
                .SelectMany(cp => Enumerable.Repeat(cp.Product, cp.Quantity))
                .GroupBy(p => p.Category)
                .Where(group => group.Count() >= 3)
                .Select(group =>
                {
                    var cheapestProduct = group.OrderBy(p => p.Price).FirstOrDefault();
                    return cheapestProduct?.Price ?? 0;
                })
                .Max();

            return maxDiscount;
        }

        public override bool IsApplicable(IEnumerable<CartProduct> products)
        {
            var categoryCounts = products.GroupBy(p => p.Product.Category)
                                         .Select(group => group.Sum(p => p.Quantity));

            return categoryCounts.Any(count => count >= 3);
        }
    }
}
