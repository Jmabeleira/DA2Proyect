using Domain.Models;

namespace PromoFidelityDlls.Models
{
    public class PromoFidelity : Promotion
    {
        public override string Name { get; } = "Promo Fidelity";
        public override string Description { get; } = "Los dos productos de menor valor son gratis";
        public override string Condition { get; } = "Tener al menos 3 productos de la misma marca";

        public PromoFidelity()
        {
            Id = Guid.NewGuid();
        }

        public override double CalculateDiscount(IEnumerable<CartProduct> cartProducts)
        {
            var maxDiscount = cartProducts
                .SelectMany(cp => Enumerable.Repeat(cp.Product, cp.Quantity))
                .GroupBy(p => p.Brand)
                .Where(group => group.Count() >= 3)
                .Select(group =>
                {
                    var cheapestProducts = group.OrderBy(p => p.Price).Take(2).ToList();
                    return cheapestProducts.Count == 2 ? cheapestProducts.Sum(p => p.Price) : 0;
                })
                .Max();

            return maxDiscount;
        }

        public override bool IsApplicable(IEnumerable<CartProduct> products)
        {
            var brandCounts = products.GroupBy(p => p.Product.Brand)
                                     .Select(group => group.Sum(p => p.Quantity));

            return brandCounts.Any(count => count >= 3);
        }
    }
}
