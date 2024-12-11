namespace Dtos.Models
{
    public class ColorDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IEnumerable<string> Colors { get; set; }
        public Guid ProductDtoId { get; set; }
        public ProductDto Product { get; set; }
    }
}
