using Domain.Models;

namespace Domain.WebModels.In
{
    public class ProductIn
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public bool IsPromotional { get; set; }

        public Product ToEntity()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Price = Price,
                Description = Description,
                Brand = Brand,
                Colors = Colors,
                Category = Category,
                IsPromotional = IsPromotional,
                Stock = Stock
            };
        }
    }
}
