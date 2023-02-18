using Advisor.Core.Domain.DTOS;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
