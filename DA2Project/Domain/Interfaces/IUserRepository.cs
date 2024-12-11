using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User ModifyUser(User entity);
        User AddUser(User entity);
        bool DeleteUserById(Guid Id);
        User GetUser(Func<User, bool> func);
        List<User> GetAllUsers();
        bool Exist(Func<User, bool> func);
    }
}
