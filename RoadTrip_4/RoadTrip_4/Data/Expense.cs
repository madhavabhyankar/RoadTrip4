using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Data
{
    public class Expense
    {
        public int Id { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        [ForeignKey("RoadTrip")]
        public Guid RoadTripId { get; set; }
        [ForeignKey("Borrower")]
        public int? BorrowerId  { get; set; }
        
        public DateTime ExpenseDate { get; set; }

        public double Amount { get; set; }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public UserDetail Owner { get; set; }
        public UserDetail Borrower { get; set; }
        public RoadTrip RoadTrip { get; set; }
    }
}