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
        Task<string> UpdateInvestment(InvestmentDTO request, int infoID, int strtID, int advId);
        Task<string> DeleteInvestment(int infoID, int strtID);

        Task<List<GetInvestments>?> GetuserInvestments(int uID);
        Task<GetInvestments?> GetSingleInvestment(int infoID, int strtID);

        Task<string?> TotalAmount(int uID);
    }
}
