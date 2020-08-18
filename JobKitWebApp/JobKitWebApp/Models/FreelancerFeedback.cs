using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class FreelancerFeedback
    {
        [Key]
        public int FreelancerFeedbackId { get; set; }
        [Required]
        public string FreelancerReply { get; set; }
        public int JobApplyId { get; set; }

        [ForeignKey("JobApplyId")]
        public ApplyJob ApplyJob { get; set; }
    }
}