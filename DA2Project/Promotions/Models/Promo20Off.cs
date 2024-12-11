using Domain.Models;

namespace Promo20OffDlls.Models
{
    public class Promo20Off : Promotion
    {
        public override string Name { get; } = "Promo 20% Off";
        public override string Description { get; } = "20% de descuento en el producto de mayor valor.";
        public override string Condition { get; } = "Si el carrito tiene 2 o más productos.";

        public Promo20Off()
        {
            Id = Guid.NewGuid();
        }

        public override double CalculateDiscount(IEnumerable<CartProduct> products)
        {
            return products.Max(p => p.Product.Price) * 0.2;
        }

        public override bool IsApplicable(IEnumerable<CartProduct> products)
        {
            return products.Count() >= 2;
        }
    }
}