using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]

        public string Name { get; set; }
        [Required]
        [MaxLength(50)]

        public string Phone { get; set; }
        [Required]
        [MaxLength(50)]
        [Index("uk_user_email", IsUnique = true)]

        public string Email { get; set; }

        public int EmailVerifiedFlag { get; set; } = -1;

        [Required]
        public string Password { get; set; }

        [NotMapped]
        public int VerificationCode { get; set; }

        public List<Job> Jobs { get; set; }

    }
}