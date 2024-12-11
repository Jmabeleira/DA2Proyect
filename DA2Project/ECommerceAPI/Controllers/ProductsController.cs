using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.WebModels.In;
using ECommerceAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(IProductService productSer, IHttpContextAccessor httpContextAccessor)
        {
            _productLogic = productSer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult AddProduct([FromBody] ProductIn productIn)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null && !userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)))
            {
                throw new InvalidPermissionException();
            }

            Product newProduct = _productLogic.AddProduct(productIn.ToEntity());
            return CreatedAtAction(nameof(AddProduct), newProduct.Name, newProduct);
        }


        [HttpGet]
        public IActionResult GetAllProducts([FromQuery] string? text, [FromQuery] string? brand, [FromQuery] string? category, [FromQuery] bool? isPromotional, [FromQuery] double? minPrice, [FromQuery] double? maxPrice)
        {
            var parametersFilter = new ParametersFilter(text, brand, category, isPromotional, minPrice, maxPrice);
            return Ok(_productLogic.GetAllProducts(parametersFilter));
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] Guid Id)
        {
            var product = _productLogic.GetProductById(Id);
            return Ok(product);
        }

        [HttpPut]
        [AuthenticationFilter]
        public IActionResult ModifyProductById([FromBody] Product product)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null && !userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)))
            {
                throw new InvalidPermissionException();
            }

            var prod = _productLogic.UpdateProduct(product);
            return Ok(prod);
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter]
        public IActionResult DeleteProductById([FromRoute] Guid Id)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null && !userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)))
            {
                throw new InvalidPermissionException();
            }

            _productLogic.DeleteProductById(Id);
            return Ok("Removed product Succesfully");
        }
    }
}
