using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class JobType
    {
        [Key]
        public int JobTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string JobTypeName { get; set; }
        public List<Job> Jobs { get; set; }
    }
}