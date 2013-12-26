using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Data
{
    public class PersonToRoadTripMap
    {

        public int Id { get; set; }
        [ForeignKey("RoadTrip")]
        public Guid RoadTripId { get; set; }
        [ForeignKey("UserDetail")]
        public int UserDetailId { get; set; }

        public RoadTrip RoadTrip { get; set; }
        public UserDetail UserDetail { get; set; }
    }
}