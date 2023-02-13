using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<Users> CreateUser(Users users)
        {
            try
            {
                _userRepository.CreateUser(users);
                return Task.FromResult(users);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<Users>?> DeleteUser(int id)
        {
            try
            {
                var user = _userRepository.DeleteUser(id);
                return Task.FromResult(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<Users>?> GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                return Task.FromResult(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Users?> GetUserById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                return Task.FromResult(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Users?> UpdateUser(Users users, int id)
        {
            try
            {
                _userRepository.UpdateUser(users, id);
                return Task.FromResult(users);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
