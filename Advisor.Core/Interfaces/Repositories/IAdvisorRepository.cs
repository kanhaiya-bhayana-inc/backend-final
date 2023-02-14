
using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using System.Reflection.Metadata;

namespace Advisor.Core.Interfaces.Repositories
{
    public interface IAdvisorRepository
    {
        AdvisorInfoToReturn? GetAdvisorById(int id);
        List<AdvisorInfoToReturn>? GetAllAdvisors();
        AdvisorInfo CreateAdvisor(AdviserDto request);
        AdvisorInfoToReturn? UpdateAdvisor(int id, AdvisorInfoToReturn advisor);
        List<AdvisorInfo>? DeleteAdvisor(int id);

        string Login(AuthAdvisorDto request);
        public string GetMyName();

        string AdvisorAccVerify(string token);

        string ForgotPasswordAdv(string email);

        string ResetPasswordAdv(AdvResetPasswordDto request);
    }
}
