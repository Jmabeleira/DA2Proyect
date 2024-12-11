using Domain.Models;

namespace PromoTotalLookDlls.Models
{
    public class PromoTotalLook : Promotion
    {
        public override string Name { get; } = "Promo Total Look";
        public override string Description { get; } = "50% de descuento en el producto de mayor valor";
        public override string Condition { get; } = "Tener al menos 3 productos del mismo color";

        public PromoTotalLook()
        {
            Id = Guid.NewGuid();
        }

        public override double CalculateDiscount(IEnumerable<CartProduct> cartProducts)
        {
            var maxDiscount = cartProducts
                .SelectMany(cp => Enumerable.Repeat(cp.Product, cp.Quantity))
                .SelectMany(p => p.Colors)
                .GroupBy(color => color)
                .Where(group => group.Count() >= 3)
                .Select(group =>
                {
                    var expensiveProduct = cartProducts
                        .SelectMany(cp => Enumerable.Repeat(cp.Product, cp.Quantity))
                        .Where(p => p.Colors.Contains(group.Key))
                        .OrderByDescending(p => p.Price)
                        .FirstOrDefault();

                    return expensiveProduct?.Price * 0.5 ?? 0;
                })
                .Max();

            return maxDiscount;
        }

        public override bool IsApplicable(IEnumerable<CartProduct> products)
        {
            var colorCounts = products
                .SelectMany(p => p.Product.Colors)
                .GroupBy(color => color)
                .Select(group => group.Sum(color => products.Where(p => p.Product.Colors.Contains(color)).Sum(p => p.Quantity)));

            return colorCounts.Any(count => count >= 3);
        }
    }
}
