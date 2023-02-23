using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Domain.DTOS
{
    // shivam added
    public class GetUserDto
    {
        public int UserID { get; set; }

        public string SortName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? AdvisorID { get; set; }

        public string Phone { get; set; } = null!;

        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public string Address { get; set; } = null;
        public string City { get; set; } = null;
        public string State { get; set; } = null;
        public string Company { get; set; } = null;


    }
}
