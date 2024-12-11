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
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthsController(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var userLogged = _authService.Login(new User { Email = userLogin.Email, Password = userLogin.Password });
            return Ok(new UserOut(userLogged));
        }

        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            var userLogged = _authService.Logout();
            return Ok(new UserOut(userLogged));
        }

        [HttpGet("currentuser")]
        [AuthenticationFilter]
        public IActionResult GetCurrentUser()
        {
            var userAuth = (User)_httpContextAccessor.HttpContext.Items["user"];

            if (userAuth is not null && !userAuth.UserRole.Any(x => x == UserRole.Admin))
            {
                throw new InvalidPermissionException();
            }

            var userLogged = _authService.GetCurrentUser();
            return Ok(new UserOut(userLogged));
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserToRegisterIn userToRegisterIn)
        {
            var userRegistered = _authService.RegisterUser(userToRegisterIn.ToEntity());
            return Ok(new UserOut(userRegistered));
        }
    }

}
