using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        User Login(User user);
        User Logout();
        User GetCurrentUser();
        User RegisterUser(User user);
    }
}
