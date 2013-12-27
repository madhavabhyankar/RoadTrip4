using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Data
{
    public enum RoadTripStatus
    {
        Active = 0,
        Deleted = 1
    }
    public class RoadTrip
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public RoadTripStatus RoadTripStatus { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        public UserDetail Owner { get; set; }

        
        public ICollection<PersonToRoadTripMap> UserInRoadTrip { get; set; }
        public ICollection<Expense> ExpensesForRoadTrip { get; set; } 
    }
}