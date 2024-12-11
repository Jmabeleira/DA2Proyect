namespace Dtos.Models
{
    public class CartProductDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public OrderDto Order { get; set; }

    }
}
