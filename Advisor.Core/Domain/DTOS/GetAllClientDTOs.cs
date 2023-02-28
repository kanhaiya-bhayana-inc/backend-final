using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Domain.DTOS
{
    public class GetAllClientDTOs
    {

        public int UserID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? SortName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? ClientID { get; set; }

        [Required, EmailAddress]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Phone { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Address { get; set; }
    }
}
