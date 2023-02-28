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
        GetClientUserDto? GetClientUserById(int id); // shivam 19Feb

        UpdateUserDto? UpdateClientUser(int id, UpdateUserDto request); // shivam

        string DeleteUser(int id); // shivam

        string DeleteClientUser(int id); // shivam
        Users CreateUser(AddUserDto request);
        string CreateClient(AddUserDto request);
        string LoginAdvisor(AuthAdvisorDto request);
        string LoginClient(AuthAdvisorDto request);
        string AdvisorAccVerify(string token);

        UpdateUserDto? UpdateUser(string id,UpdateUserDto request);

        string ResetPasswordUser(UserResetPasswordDto request);
        string ForgotPasswordUser(string email);
        string ChangePassword();

        List<GetAllClientDTOs>? GetAllClients(int id);

        List<int>? GetAllIDS(int id);

    }
}
