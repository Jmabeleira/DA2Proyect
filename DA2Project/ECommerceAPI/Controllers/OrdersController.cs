using Domain.Interfaces;
using Domain.Models;
using Domain.WebModels.In;
using Domain.WebModels.Out;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Filters;
using Domain.Exceptions;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrdersController(IOrderService orderService, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AuthenticationFilter]
        public IActionResult GetAllOrders([FromQuery] Guid? customerId)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];

            if (customerId != null && userLogged != null && (customerId == userLogged.Id || userLogged.UserRole.Any(x => x.Equals(UserRole.Admin))))
            {
                List<Order> userOrders = _orderService.GetOrdersByCustomerId(customerId.Value);
                return Ok(userOrders.Select(x => new OrderOut(x)).ToList());
            }
            else if (userLogged != null && !userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)))
            {
                throw new InvalidPermissionException();
            }

            List<Order> orders = _orderService.GetAllOrders();
            return Ok(orders.Select(x => new OrderOut(x)).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderByUserId([FromRoute] Guid id)
        {
            List<OrderOut> orderOut = _orderService.GetOrderByUserId(id).Select(x => new OrderOut(x)).ToList();

            return Ok(orderOut);
        }

        [HttpPost]
        [Route("Purchase")]
        public IActionResult Purchase([FromBody] OrderIn orderIn, [FromQuery] string paymentMethod)
        {
            OrderOut orderOut = new OrderOut(_orderService.Purchase(orderIn.ToEntity(), paymentMethod));

            return Ok(orderOut);
        }
    }
}
