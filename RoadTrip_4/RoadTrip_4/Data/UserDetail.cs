using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Data
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }

        [ForeignKey("UserProfile")]
        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<PersonToRoadTripMap> RoadTripsForUser { get; set; }
        public ICollection<RoadTrip> RoadTripsCreatedByUser { get; set; } 
    }
}