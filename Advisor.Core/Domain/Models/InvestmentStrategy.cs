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
    public class InvestmentStrategy
    {
        [Key]
        public int InvestmentStrategyID { get; set; }

        [Display(Name = "InvestorInfo")]
        public virtual int InvestorInfoID { get; set; }
        [ForeignKey("InvestorInfoID")]
        public virtual InvestorInfo? InvestorInfos { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string StrategyName { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(6)]
        public string AccountID { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(6)]
        public string ModelAPLID { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(17,5)")]
        public decimal InvestmentAmount { get; set; }

        [Display(Name = "InvestmentType")]
        public virtual int InvestmentTypeID { get; set; }
        [ForeignKey("InvestmentTypeID")]
        public virtual InvestmentType? InvestmentTypes { get; set; }

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
