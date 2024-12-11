namespace Dtos.Models
{
    public class BrandDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Guid ProductDtoId { get; set; }
        public ProductDto Product { get; set; }
    }
}
