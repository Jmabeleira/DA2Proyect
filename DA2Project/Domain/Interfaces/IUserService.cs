using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        User AddUser(User user);
        bool DeleteUserById(Guid Id);
        List<User> GetAllUsers();
        User GetUserById(Guid Id);
        User ModifyUser(User user);
        User GetUserByToken(Guid aToken);
        bool IsValidToken(Guid aToken);
    }
}
