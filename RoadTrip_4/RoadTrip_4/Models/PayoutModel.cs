using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Models
{
    public class PayoutModel
    {
        public int PayToUserId { get; set; }
        public string PayToName { get; set; }
        public double Amount { get; set; }
    }
}