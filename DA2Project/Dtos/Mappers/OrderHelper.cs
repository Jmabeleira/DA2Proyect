using Domain.Models;
using Dtos.Models;

namespace Dtos.Mappers
{
    public static class OrderHelper
    {
        public static Order FromOrderDtoToOrder(OrderDto orderDto)
        {
            return new Order
            {
                Id = orderDto.Id,
                Customer = UserHelper.FromUserDtoToUser(orderDto.Customer),
                Date = orderDto.Date,
                Products = orderDto.Products.Select(cp => CartProductHelper.FromCartProductDtoToCartProduct(cp)).ToList(),
                PaymentMethod = orderDto.PaymentMethod != null ? orderDto.PaymentMethod.FromPaymentMethodDtoToPaymentMethod() : null,
                TotalPrice = orderDto.TotalPrice,
                AppliedPromotion = orderDto.AppliedPromotion != null ?
                                        PromotionHelper.FromAppliedPromotionDtoToAppliedPromotion(orderDto.AppliedPromotion) : null
            };
        }

        public static OrderDto FromOrderToOrderDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Customer = UserHelper.FromUserToUserDto(order.Customer),
                Date = order.Date,
                Products = order.Products.Select(cp => CartProductHelper.FromCartProductToCartProductDto(cp)).ToList(),
                PaymentMethod = PaymentMethodHelper.FromPaymentToPaymentDto(order.PaymentMethod),
                TotalPrice = order.TotalPrice,
                AppliedPromotion = PromotionHelper.FromAppliedPromotionToAppliedPromotionDto(order.AppliedPromotion)
            };
        }
    }
}
