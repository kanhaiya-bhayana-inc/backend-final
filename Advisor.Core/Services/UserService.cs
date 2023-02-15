using Advisor.Core.Domain.DTOS;
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

        public string AdvisorAccVerify(string token)
        {
            try
            {
                var obj = _userRepository.AdvisorAccVerify(token);
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ChangePassword()
        {
            try
            {
                var obj = _userRepository.ChangePassword();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Users> CreateUser(AddUserDto request)
        {
            try
            {
                var res = _userRepository.CreateUser(request);
                return Task.FromResult(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgotPasswordUser(string email)
        {
            try
            {
                var res = _userRepository.ForgotPasswordUser(email);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Login(AuthAdvisorDto request)
        {
            try
            {
                var obj = _userRepository.Login(request);
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ResetPasswordUser(UserResetPasswordDto request)
        {
            try
            {
                var obj = _userRepository.ResetPasswordUser(request);
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateUserDto? UpdateUser(string id, UpdateUserDto request)
        {
            try
            {
                var obj = _userRepository.UpdateUser(id,request);
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    
}
