namespace Domain.Models
{
    public class ParametersFilter
    {
        public string? Text { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public bool? IsPromotional { get; set; }
        public Tuple<double?, double?> PriceRange { get; set; }

        public ParametersFilter(string? text, string? brand, string? category, bool? isPromotional, double? minPrice, double? maxPrice)
        {
            Text = text;
            Brand = brand;
            Category = category;
            IsPromotional = isPromotional;
            PriceRange = new Tuple<double?, double?>(maxPrice, minPrice);
        }

        public List<Func<Product, bool>> GetQueryFilters()
        {
            List<Func<Product, bool>> filters = new List<Func<Product, bool>>();

            if (!string.IsNullOrWhiteSpace(Text))
            {
                filters.Add(p => p.Name.ToLower().Contains(Text.ToLower()) || p.Category.ToLower().Contains(Text.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(Brand))
            {
                filters.Add(p => p.Brand.ToLower().Contains(Brand.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(Category))
            {
                filters.Add(p => p.Category.ToLower().Contains(Category.ToLower()));
            }

            if (IsPromotional != null)
            {
                filters.Add(p => p.IsPromotional.Equals(IsPromotional.Value));
            }

            if (PriceRange?.Item1 != null)
            {
                filters.Add(p => p.Price <= PriceRange.Item1);
            }

            if (PriceRange?.Item2 != null)
            {
                filters.Add(p => p.Price >= PriceRange.Item2);
            }

            return filters;
        }
    }
}
