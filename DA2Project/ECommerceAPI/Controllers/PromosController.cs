using Domain.Interfaces;
using ECommerceAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class PromosController : ControllerBase
    {
        private readonly IPromoService _promoService;

        public PromosController(IPromoService promoService)
        {
            _promoService = promoService;
        }

        [HttpGet("{id}")]
        public IActionResult GetPromo([FromRoute] Guid id)
        {
            var promo = _promoService.GetPromo(id);
            return Ok(promo);

        }

        [HttpGet]
        public IActionResult GetAllPromos()
        {
            var promos = _promoService.GetAllPromos();
            return Ok(promos);
        }
    }
}
