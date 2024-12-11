using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Managers
{
    public class AuthManager : IAuthManager
    {
        private static User _currentUser;
        private readonly IUserRepository _userRepository;

        public AuthManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetCurrentUser()
        {
            if (_currentUser == null)
            {
                throw new ArgumentException("No user logged in");
            }

            return _currentUser;
        }

        public User Login(User user)
        {
            var dbUser = _userRepository.GetUser((User u) => u.Email == user.Email && u.Password == user.Password);
            if (dbUser != null)
            {
                _currentUser = dbUser;
                return dbUser;
            }

            throw new ArgumentException("Invalid credentials");
        }

        public User Logout()
        {
            var userLogedOut = _currentUser;
            _currentUser = null;
            return userLogedOut;
        }
    }
}
