using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class Connect
    {
        [Key]
        public int ConnectId { get; set; }
        public int ConnectPriceId { get; set; }
        public int FreelancerId { get; set; }
        public string Refernce { get; set; }
        public int PaymentStatus { get; set; } = -1;

        [ForeignKey("ConnectPriceId")]
        public ConnectPricing ConnectPricing { get; set; }
        [ForeignKey("FreelancerId")]
        public Freelancer Freelancer { get; set; }
    }
}