using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobKitWebApp.Models
{
    public class ConnectPricing
    {
        [Key]
        public int ConnectPricingId { get; set; }
        public int NumberOfConnect { get; set; }
        public double Price { get; set; }
        public List<Connect> Connects { get; set; }
    }
}