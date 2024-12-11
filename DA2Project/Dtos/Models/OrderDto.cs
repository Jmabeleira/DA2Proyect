using Dtos.Models.PaymentMethods;

namespace Dtos.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto Customer { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CartProductDto> Products { get; set; }
        public PaymentMethodDto PaymentMethod { get; set; }
        public double TotalPrice { get; set; }
        public AppliedPromotionDto AppliedPromotion { get; set; }
    }
}
