using Advisor.Core.Domain.DTOS;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentService(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }
        public Task<string> AddInvestment(InvestmentDTO request, int id)
        {
            try
            {
                var res = _investmentRepository.AddInvestment(request, id);
                return Task.FromResult(res);
            }
            catch(Exception) { throw; }
        }

        public Task<string> DeleteInvestment(int infoID, int strtID)
        {
            try
            {
                var res = _investmentRepository.DeleteInvestment(infoID, strtID);
                return Task.FromResult(res);
            }
            catch (Exception) { throw; }
        }

        public Task<List<GetInvestments>?> GetuserInvestments(int uID)
        {
            try
            {
                var res = _investmentRepository.GetuserInvestments(uID);
                return Task.FromResult(res);
            }
            catch (Exception) { throw; }
        }

        public Task<string> UpdateInvestment(InvestmentDTO request, int infoID, int strtID, int advId)
        {
            try
            {
                var res = _investmentRepository.UpdateInvestment(request, infoID,strtID,advId);
                return Task.FromResult(res);
            }
            catch (Exception) { throw; }
        }
    }
}
