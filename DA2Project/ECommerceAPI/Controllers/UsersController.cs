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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IUserService userLogic, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userLogic;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [AuthenticationFilter]
        public IActionResult AddUser([FromBody] AddUserIn userIn)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null && !userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)))
            {
                throw new InvalidPermissionException();
            }

            UserOut userOut = new UserOut(_userService.AddUser(userIn.ToEntity()));
            return CreatedAtAction(nameof(AddUser), userOut.Email, userOut);
        }

        [HttpGet]
        [AuthenticationFilter]
        public IActionResult GetAllUsers()
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null && !userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)))
            {
                throw new InvalidPermissionException();
            }

            var users = _userService.GetAllUsers();

            if (users.Any())
            {
                return Ok(users.Select(x => new UserOut(x)).ToList());
            }
            return NoContent();
        }

        [HttpGet("{Id}")]
        [AuthenticationFilter]
        public IActionResult GetUserById(Guid Id)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null)
            {
                if (!(userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)) || userLogged.Id == Id))
                {
                    throw new InvalidPermissionException();
                }
            }

            var user = _userService.GetUserById(Id);
            return Ok(new UserOut(user));
        }

        [HttpPut("{Id}")]
        [AuthenticationFilter]
        public IActionResult ModifyUserById(Guid Id, [FromBody] UserIn userIn)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null)
            {
                if (!(userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)) || userLogged.Id == Id))
                {
                    throw new InvalidPermissionException();
                }
            }

            var user = userIn.ToEntity();
            user.Id = Id;

            var userResult = _userService.ModifyUser(user);
            return Ok(new UserOut(userResult));
        }

        [HttpDelete("{Id}")]
        [AuthenticationFilter]
        public IActionResult DeleteUserById(Guid Id)
        {
            var userLogged = (User)_httpContextAccessor.HttpContext.Items["user"];
            if (userLogged is not null && !userLogged.UserRole.Any(x => x.Equals(UserRole.Admin)))
            {
                throw new InvalidPermissionException();
            }

            _userService.DeleteUserById(Id);

            return Ok("User deleted successfully");
        }
    }
}
