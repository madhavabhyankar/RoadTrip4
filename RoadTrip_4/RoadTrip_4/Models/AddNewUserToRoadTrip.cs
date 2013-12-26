using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Models
{
    public class AddNewUserToRoadTrip
    {
        public string Email { get; set; }
        public Guid RoadTripId { get; set; }
        public int OwnerId { get; set; }
    }
}