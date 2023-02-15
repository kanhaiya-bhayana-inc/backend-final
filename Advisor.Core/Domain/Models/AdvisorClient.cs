using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Domain.Models
{
    public class AdvisorClient
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("AdvisorID")]
        public virtual Users Advisor { get; set; }
        [ForeignKey("ClientID")]
        public virtual Users Client { get; set; }


    }
}
