﻿using Advisor.Core.Domain.DTOS;
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

        public string CreateClient(AddUserDto request)
        {
            try
            {
                var res = _userRepository.CreateClient (request);
                return res;
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

        public Task<List<GetAllClientDTOs>?> GetAllClients(int id)
        {
            try
            {
                var users = _userRepository.GetAllClients(id);
                return Task.FromResult(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<int>?> GetAllIDS(int id)
        {
            try
            {
                var users = _userRepository.GetAllIDS(id);
                return Task.FromResult(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<GetUserDto?> GetUserByAuth()
        {
            try
            {
                var res = _userRepository.GetUserByAuth();
                return Task.FromResult(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LoginAdvisor(AuthAdvisorDto request)
        {
            try
            {
                var obj = _userRepository.LoginAdvisor(request);
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LoginClient(AuthAdvisorDto request)
        {
            try
            {
                var obj = _userRepository.LoginClient(request);
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

        public GetClientUserDto? GetClientUserById(int id)  // shivam 19Feb
        {
            try
            {
                var result = _userRepository.GetClientUserById(id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteClientUser(int id) // shivam
        {
            try
            {
                var obj = _userRepository.DeleteClientUser(id);

                return obj;
            }
            catch (Exception) { throw; }
        }

        public UpdateUserDto? UpdateClientUser(int id, UpdateUserDto request)
        {
            try
            {
                var obj = _userRepository.UpdateClientUser(id, request);
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        } // shivam


        public string DeleteUser(int id)
        {
            try
            {
                var obj = _userRepository.DeleteUser(id);

                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        } //shivam
    }
    
}
