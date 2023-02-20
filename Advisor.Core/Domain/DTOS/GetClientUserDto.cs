using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Domain.DTOS
{
    public class GetClientUserDto // shivam 19Feb
    {
        public string? ClientID { get; set; }
        public string SortName { get; set; } = null!;
        public string Email { get; set; } = null!;

        

        public string Phone { get; set; } = null!;
    }
}
