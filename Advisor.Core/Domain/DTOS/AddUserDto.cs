using Advisor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Advisor.Core.Domain.DTOS
{
    public class AddUserDto
    {
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required,EmailAddress]
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string Email { get; set; } = null!;

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string? Address { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string? City { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string? State { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(40)]
        public string Phone { get; set; } = null!;

        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string? Company { get; set; }

        [Required, MinLength(6)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string? Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
