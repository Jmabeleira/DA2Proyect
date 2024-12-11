using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Exceptions;
using Logic.Exceptions;

namespace Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRrepository;

        public UserService(IUserRepository userRrepository)
        {
            _userRrepository = userRrepository;
        }

        public User AddUser(User user)
        {
            try
            {
                user.Timestamp = DateTime.Now;
                user.Token = Guid.NewGuid();
                return _userRrepository.AddUser(user);
            }
            catch (DANotUniqueException ex)
            {
                throw new AlreadyExistsException("User already exists", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return _userRrepository.GetAllUsers();
            }
            catch (DANoInstanceException ex)
            {
                throw new NoContentException("There are no Users", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public User GetUserById(Guid Id)
        {
            try
            {
                return _userRrepository.GetUser((User u) => u.Id == Id);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("There is no such User", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public User ModifyUser(User userIn)
        {
            try
            {
                return _userRrepository.ModifyUser(userIn);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("User does not exist", ex);
            }
            catch (DANotUniqueException ex)
            {
                throw new AlreadyExistsException("Mail already in use", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public bool DeleteUserById(Guid guid)
        {
            try
            {
                return _userRrepository.DeleteUserById(guid);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("User does not exist", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public User GetUserByToken(Guid aToken)
        {
            try
            {
                return _userRrepository.GetUser((User u) => u.Token == aToken);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("User does not exist", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public bool IsValidToken(Guid aToken)
        {
            try
            {
                return _userRrepository.Exist((User u) => u.Token == aToken);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }
    }
}
