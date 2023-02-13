using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Domain.DTOS
{
    public class AdviserDto
    {
        

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]

        public string? Password { get; set; }
    }
}
