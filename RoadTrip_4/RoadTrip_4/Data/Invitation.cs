using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Data
{
    public enum InvitationStatus
    {
        Invited =0,
        Registerd = 1
    }
    public class Invitation
    {
        public int Id { get; set; }
        public int InvitingUserDetailId { get; set; }
        public string GuestEmail { get; set; }
        public InvitationStatus InvitationStatus { get; set; }


    }
}