using Advisor.Core.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Interfaces.Services
{
    public interface IInvestmentService
    {
        Task<string> AddInvestment(InvestmentDTO request,int id);
    }
}
