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

        public string? investmentName { get; set; }
        public string? typeName { get; set; }
        public string? strategyName { get; set; }

        public int active { get; set;}

        public decimal? totalAmount { get;set; }
        public string? acountID { get; set;}

        public decimal? investmentAmount { get;set; }
    }
}
