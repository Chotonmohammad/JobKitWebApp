using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class Freelancer
    {
        [Key]
        public int FreelancerId { get; set; }

        public string FreelancerName { get; set; }

        public string FreelancerTitle { get; set; }

        public string FreelancerIntroduction { get; set; }

        public int? FreelancerCategoryId { get; set; }

        public int? CityId { get; set; }

        public int TotalConnect { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        [MaxLength(100)]
        [Index("uk_freelancer_email",IsUnique =true)]
        public string Email { get; set; }

        public int EmailVerified { get; set; } = -1;

        public int VerifiedFlag { get; set; } = -1;

        public string Password { get; set; }

        public string PhotoUrl { get; set; }

        [NotMapped]
        public int VerificationCode { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        [ForeignKey("FreelancerCategoryId")]
        public FreelancerCategory FreelancerCategory { get; set; }
        public List<ApplyJob> ApplyJobs { get; set; }
        public List<Connect> Connects { get; set; }
    }
}