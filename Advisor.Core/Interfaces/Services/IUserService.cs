using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Interfaces.Services
{
    public interface IUserService
    {
        
        Task<Users> CreateUser(AddUserDto request);

        string Login(AuthAdvisorDto request);

        string AdvisorAccVerify(string token);

        UpdateUserDto? UpdateUser(string id, UpdateUserDto request);
        string ResetPasswordUser(UserResetPasswordDto request);
        string ForgotPasswordUser(string email);

    }
}
