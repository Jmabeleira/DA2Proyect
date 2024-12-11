using Domain.Models;
using Dtos.Models;

namespace Dtos.Mappers
{
    public static class CartProductHelper
    {
        public static CartProduct FromCartProductDtoToCartProduct(CartProductDto cartProductDto)
        {
            return new CartProduct
            {
                Id = cartProductDto.Id,
                Product = ProductHelper.FromProductDtoToProduct(cartProductDto.Product),
                Quantity = cartProductDto.Quantity
            };
        }

        public static CartProductDto FromCartProductToCartProductDto(CartProduct cartProduct)
        {
            return new CartProductDto
            {
                Id = cartProduct.Id,
                Product = ProductHelper.FromProductToProductDto(cartProduct.Product),
                Quantity = cartProduct.Quantity
            };
        }
    }
}
