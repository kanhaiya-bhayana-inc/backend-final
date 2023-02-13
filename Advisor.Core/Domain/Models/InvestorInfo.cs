using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Advisor.Core.Domain.Models
{
    public class InvestorInfo
    {
        [Key]
        public int InvestorInfoID { get; set; }

        [Display(Name = "Users")]
        public virtual int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual Users? Userss { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string? InvestmentName { get; set; }

        [Required]
        [Column(TypeName = "Tinyint")]
        public int Active { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? ModifiedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? ModifiedDate { get; set; }

        [Required]
        public int DeletedFlag { get; set; }

        [NotMapped]
        public bool DeletedFlagbool
        {
            get { return (DeletedFlag == 1); }
        }
    }
}
