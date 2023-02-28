using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Domain.DTOS
{
    public class GetInvestments
    {
        public int inofID { get; set; }

        public int strategyID { get; set; }

        public string? InvestmentName { get; set; }
        public string? InvestmentTypeName { get; set; }
        public string? StrategyName { get; set; }

        public int Active { get; set;}

        public decimal? totalAmount { get;set; }
        public string? AccountID { get; set;}

        public decimal? InvestmentAmount { get;set; }
    }
}
