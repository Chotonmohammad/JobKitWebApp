using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class UserFeedback
    {
        [Key]
        public int UserFeedbackId { get; set; }
        [Required]
        public string UserReply { get; set; }
        public int JobApplyId { get; set; }

        [ForeignKey("JobApplyId")]
        public ApplyJob ApplyJob { get; set; }
    }
}