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
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string RoleName { get; set; } = null!;

        [Required]
        [Column(TypeName = "Tinyint")]
        public int Active { get; set; }

        [JsonIgnore]
        List<Users> Users { get; set; } = new List<Users>();
    }
}

