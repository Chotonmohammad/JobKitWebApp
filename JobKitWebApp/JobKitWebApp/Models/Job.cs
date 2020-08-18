using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]

        public string JobDescription { get; set; }
        [Required]

        public int JobTypeId { get; set; }
        [Required]

        public int FreelancerCategoryId { get; set; }
        [Required]

        public double Budget { get; set; }
        [Required]

        public int CityId { get; set; }
        [Required]

        public int UserId { get; set; }

        public int JobActiveFlag { get; set; } = -1;

        public int WorkProgess { get; set; } = 0;


        [ForeignKey("CityId")]

        public City City { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("FreelancerCategoryId")]
        public FreelancerCategory FreelancerCategory { get; set; }
        [ForeignKey("JobTypeId")]
        public JobType JobType { get; set; }
        public List<ApplyJob> ApplyJobs { get; set; }

    }
}