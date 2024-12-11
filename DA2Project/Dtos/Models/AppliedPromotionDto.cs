namespace Dtos.Models
{
    public class AppliedPromotionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public Guid OrderId { get; set; }
        public OrderDto Order { get; set; }
    }
}
