using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Dtos.Interfaces;
using Dtos.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IContextDb _dbContext;

        public UserRepository(IContextDb userContext)
        {
            _dbContext = userContext;
        }

        public User AddUser(User entity)
        {
            try
            {
                if (!IsUnique(entity))
                {
                    throw new InstanceNotUniqueException("User is not unique");
                }

                _dbContext.Users.Add(UserHelper.FromUserToUserDto(entity));
                _dbContext.SaveChanges();

                return entity;
            }
            catch (InstanceNotUniqueException inuEx)
            {
                throw inuEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        private bool IsUnique(User entity)
        {
            try
            {
                return !GetAllUsers().Any(x => x.Email == entity.Email);
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool DeleteUserById(Guid Id)
        {
            try
            {
                if (Exist((User u) => u.Id == Id))
                {
                    var user = _dbContext.Users.FirstOrDefault(x => x.Id == Id);
                    _dbContext.Users.Remove(user);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new NoInstanceException("User not found");
                }
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public bool Exist(Func<User, bool> func)
        {
            try
            {
                var users = GetAllUsers();
                return users.Any(func);
            }
            catch (UnhandledDataAccessException dbEx)
            {
                throw dbEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                var users = _dbContext.Users.Include(x => x.Address).ToList();

                if (users.Count() == 0)
                    throw new NoInstanceException("There are no Users");

                return users.Select(x => UserHelper.FromUserDtoToUser(x)).ToList();
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public User GetUser(Func<User, bool> func)
        {
            try
            {
                if (Exist(func))
                {
                    var users = GetAllUsers();
                    return users.FirstOrDefault(func);
                }
                else
                {
                    throw new NoInstanceException("User not found");
                }
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public User ModifyUser(User entity)
        {
            try
            {
                if (Exist((User u) => u.Id == entity.Id))
                {
                    var userDto = UserHelper.FromUserToUserDto(entity);
                    var userDB = _dbContext.Users.FirstOrDefault(x => x.Id == entity.Id);
                    var addressDB = _dbContext.Addresses.FirstOrDefault(x => x.Id == userDB.Address.Id);

                    userDB.Email = userDto.Email;
                    userDB.Password = userDto.Password;
                    userDB.UserRole = userDto.UserRole;
                    userDB.Timestamp = userDto.Timestamp;
                    userDB.Token = userDto.Token;

                    addressDB.City = userDto.Address.City;
                    addressDB.Country = userDto.Address.Country;
                    addressDB.Street = userDto.Address.Street;
                    addressDB.ZipCode = userDto.Address.ZipCode;
                    addressDB.DoorNumber = userDto.Address.DoorNumber;

                    if (_dbContext.Users.Any(u => u.Id != userDto.Id && u.Email == userDto.Email))
                    {
                        throw new InstanceNotUniqueException("The email is already in use");
                    }
                    _dbContext.Users.Update(userDB);
                    _dbContext.SaveChanges();

                    return GetUser((User u) => u.Id == userDto.Id);
                }
                else
                {
                    throw new NoInstanceException("User not found");
                }
            }
            catch (UnhandledDataAccessException dbEx)
            {
                throw dbEx;
            }
            catch (InstanceNotUniqueException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }
    }
}