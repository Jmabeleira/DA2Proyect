using Domain.Interfaces;
using Domain.Models;
using Logic.Exceptions;

namespace Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthManager _authManager;
        private readonly IUserService _userService;

        public AuthService(IAuthManager authManager, IUserService userService)
        {
            _authManager = authManager;
            _userService = userService;
        }

        public User GetCurrentUser()
        {
            try
            {
                return _authManager.GetCurrentUser();
            }
            catch (Exception ex)
            {
                throw new CurrentUserException("No user logged in", ex);
            }
        }

        public User Login(User user)
        {
            try
            {
                return _authManager.Login(user);
            }
            catch (Exception ex)
            {
                throw new InvalidCredentialsException("Invalid credentials", ex);
            }
        }

        public User Logout()
        {
            try
            {
                return _authManager.Logout();
            }
            catch (Exception ex)
            {
                throw new CurrentUserException("No user logged in", ex);
            }
        }

        public User RegisterUser(User user)
        {
            try
            {
                return _userService.AddUser(user);
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }
    }
}
