namespace Dtos.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public BrandDto? BrandDto { get; set; }
        public ColorDto? ColorDto { get; set; }
        public CategoryDto? CategoryDto { get; set; }
        public ICollection<CartProductDto> CartProducts { get; set; }
        public int Stock { get; set; }
        public bool IsPromotional { get; set; }
    }
}
