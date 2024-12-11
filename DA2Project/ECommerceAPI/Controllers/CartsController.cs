using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.WebModels.In;
using Domain.WebModels.Out;
using ECommerceAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{Id}")]
        public IActionResult GetCartById([FromRoute] Guid Id)
        {
            return Ok(new CartOut(_cartService.GetCartById(Id)));
        }

        [HttpGet]
        public IActionResult GetCartByUserId([FromQuery] Guid UserId)
        {
            return Ok(new CartOut(_cartService.GetCartByUserId(UserId)));
        }

        [HttpPost]
        public IActionResult AddCart([FromBody] CartIn cartIn)
        {
            var user = cartIn.User;

            if (!user.UserRole.Any(r => r == UserRole.Client))
            {
                throw new InvalidPermissionException();
            }

            var cart = _cartService.AddCart(cartIn.ToEntity());
            return CreatedAtAction(nameof(CartsController.AddCart), null, new CartOut(cart));
        }

        [HttpPut("{Id}")]
        public IActionResult ModifyCart([FromRoute] Guid Id, [FromBody] CartIn cartIn)
        {
            var cart = cartIn.ToEntity();
            cart.Id = Id;

            var cartResult = _cartService.ModifyCart(cart);

            return Ok(new CartOut(cartResult));
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteCartById([FromRoute] Guid Id)
        {
            _cartService.DeleteCartById(Id);
            return Ok("Cart Deleted successfully");
        }

        [HttpPost("AddProduct/{cartId}")]
        public IActionResult AddProductToCart([FromRoute] Guid cartId, [FromBody] Product product)
        {
            var cartResult = _cartService.AddProductToCart(cartId, product);
            return CreatedAtAction(nameof(CartsController.AddProductToCart), null, new CartOut(cartResult));
        }
    }
}
