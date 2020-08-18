using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class ApplyJobConversation
    {
        [Key]
        public int ApplyJobConversationId { get; set; }
        [Required]

        public string Reply { get; set; }
        [Required]

        public int JobApplyId { get; set; }

        public int ConversationTypeFlag { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;


        [ForeignKey("JobApplyId")]
        public ApplyJob ApplyJob { get; set; }
    }
}