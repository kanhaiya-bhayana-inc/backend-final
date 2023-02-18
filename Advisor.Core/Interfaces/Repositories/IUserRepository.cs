using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        GetUserDto? GetUserByAuth();
        Users CreateUser(AddUserDto request);
        string CreateClient(AddUserDto request);
        string Login(AuthAdvisorDto request);
        string AdvisorAccVerify(string token);

        UpdateUserDto? UpdateUser(string id,UpdateUserDto request);

        string ResetPasswordUser(UserResetPasswordDto request);
        string ForgotPasswordUser(string email);
        string ChangePassword();

        List<GetAllClientDTOs>? GetAllClients(int id);

        List<int>? GetAllIDS(int id);

    }
}
