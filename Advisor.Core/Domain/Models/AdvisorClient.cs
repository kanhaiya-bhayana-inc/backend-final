using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Advisor.Core.Domain.Models
{
    public class AdvisorClient
    {
        [Key]
        public int ID { get; set; }

        public int AdvisorID { get; set; }
        public int ClientID { get; set; }

    }
}
