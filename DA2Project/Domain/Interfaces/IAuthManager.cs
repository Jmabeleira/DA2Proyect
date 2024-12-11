using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAuthManager
    {
        User GetCurrentUser();
        User Login(User user);
        User Logout();
    }
}