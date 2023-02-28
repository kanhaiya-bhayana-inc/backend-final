using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Domain.Models
{
    public class AdvisorInfo
    {
        [Key]
        public int AdvisorId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Username { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Email { get; set; }


        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string? VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }
    }
}
