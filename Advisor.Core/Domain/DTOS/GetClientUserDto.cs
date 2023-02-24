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
        public int userID { get; set; }
        public string SortName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string company { get; set; } = null!;
        public string address { get; set; } = null!;
        public string state { get; set; } = null!;

        public string city { get; set; } = null!;
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
