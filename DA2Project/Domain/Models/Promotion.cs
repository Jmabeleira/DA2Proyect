namespace Domain.Models
{
    public abstract class Promotion
    {
        public Guid Id { get; set; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Condition { get; }

        public abstract double CalculateDiscount(IEnumerable<CartProduct> products);
        public abstract bool IsApplicable(IEnumerable<CartProduct> products);

        public override bool Equals(object obj)
        {
            if (obj is Promotion promotion)
            {
                return promotion.Name == Name &&
                    promotion.Description == Description &&
                    promotion.Condition == Condition;
            }

            return false;
        }
    }
}
