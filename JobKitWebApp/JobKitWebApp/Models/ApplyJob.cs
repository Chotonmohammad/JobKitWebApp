using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class ApplyJob
    {
        [Key]
        public int ApplyJobId { get; set; }
        [Required]
        [Index("uk_jobId_freelancerId",Order =1, IsUnique = true)]
        public int JobId { get; set; }
        [Required]
        [Index("uk_jobId_freelancerId",Order =2, IsUnique = true)]
        public int FreelancerId { get; set; }
        [Required]

        public string CoverMessage { get; set; }
        [Required]

        public int JobConfirmFlag { get; set; } = 0;

        public DateTime DateTime { get; set; } = DateTime.Now;

        [ForeignKey("JobId")]
        public Job Job { get; set; }
        [ForeignKey("FreelancerId")]
        public Freelancer Freelancer { get; set; }
        public List<FreelancerFeedback> FreelancerFeedbacks { get; set; }
        public List<UserFeedback> UserFeedbacks { get; set; }

        public List<ApplyJobConversation> ApplyJobConversations { get; set; }

    }
}