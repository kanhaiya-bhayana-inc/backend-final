using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Interfaces.Services
{
    public interface IAdvisorService
    {
        Task<AdvisorInfoToReturn?> GetAdvisorById(int id);
        Task<List<AdvisorInfoToReturn>?> GetAllAdvisors();
        Task<AdvisorInfo> CreateAdvisor(AdviserDto request);
        Task<AdvisorInfoToReturn?> UpdateAdvisor(int id, AdvisorInfoToReturn advisor);
        Task<List<AdvisorInfo>?> DeleteAdvisor(int id);
        string Login(AuthAdvisorDto request);

        public string GetMyName();

        string AdvisorAccVerify(string token);
        string ForgotPasswordAdv(string email);

        string ResetPasswordAdv(AdvResetPasswordDto request);
    }
}
