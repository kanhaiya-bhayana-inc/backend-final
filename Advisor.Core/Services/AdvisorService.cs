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
    public class AdvisorService : IAdvisorService
    {
        private readonly IAdvisorRepository _advisorRepository;

        public AdvisorService(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }
        public Task<AdvisorInfo> CreateAdvisor(AdviserDto request)
        {
            try
            {
               var res = _advisorRepository.CreateAdvisor(request);
                return Task.FromResult(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<AdvisorInfo>?> DeleteAdvisor(int id)
        {
            try
            {
                var user = _advisorRepository.DeleteAdvisor(id);
                return Task.FromResult(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<AdvisorInfoToReturn?> GetAdvisorById(int id)
        {
            try
            {
                var user = _advisorRepository.GetAdvisorById(id);
                return Task.FromResult(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<AdvisorInfoToReturn>?> GetAllAdvisors()
        {
            try
            {
                var users = _advisorRepository.GetAllAdvisors();
                return Task.FromResult(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<AdvisorInfoToReturn?> UpdateAdvisor(int id, AdvisorInfoToReturn advisor)
        {
            try
            {
                var adv = _advisorRepository.UpdateAdvisor(id, advisor);
                return Task.FromResult(adv);
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
                var obj = _advisorRepository.Login(request);
                return obj;
            }
            catch(Exception)
            {
                throw;
            }
        }


        public string GetMyName()
        {
            try
            {
                var obj = _advisorRepository.GetMyName();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AdvisorAccVerify(string token)
        {
            try
            {
                var obj = _advisorRepository.AdvisorAccVerify(token);
                return obj;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public string ForgotPasswordAdv(string email)
        {
            try
            {
                var obj = _advisorRepository.ForgotPasswordAdv(email);
                return obj;
            }
            catch(Exception ) {
                throw;
            }
        }

        public string ResetPasswordAdv(AdvResetPasswordDto request)
        {
            try
            {
                var obj = _advisorRepository.ResetPasswordAdv(request);
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
