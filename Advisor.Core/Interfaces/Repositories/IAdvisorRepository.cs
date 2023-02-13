
using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;



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
    }
}
