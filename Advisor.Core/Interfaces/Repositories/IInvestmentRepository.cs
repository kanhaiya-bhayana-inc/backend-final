using Advisor.Core.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Interfaces.Repositories
{
    public interface IInvestmentRepository
    {
        string AddInvestment(InvestmentDTO request, int id);
        string UpdateInvestment(InvestmentDTO request, int infoID, int strtID, int advId);
        string DeleteInvestment(int infoID, int strtID);
    }
}
