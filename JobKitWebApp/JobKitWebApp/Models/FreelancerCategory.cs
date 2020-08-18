using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class FreelancerCategory
    {
        [Key]
        public int FreelancerCategoryId { get; set; }

        public string FreelancerCategoryName { get; set; }

        public List<Freelancer> Freelancers { get; set; }
    }
}