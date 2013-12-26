using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoadTrip_4.Data;
using RoadTrip_4.Models;

namespace RoadTrip_4.Controllers
{
    public class UsersInRoadTripController : ApiController
    {
        private readonly IRoadTripRepo _repo;

        public UsersInRoadTripController(IRoadTripRepo repo)
        {
            _repo = repo;
        }

        // GET api/usersinroadtrip/5
        public IEnumerable<UserDetail> Get(Guid id)
        {
            return _repo.GetUsersForRoadTrip(id);
        }

        // POST api/usersinroadtrip
        public HttpResponseMessage Post([FromBody]AddNewUserToRoadTrip  newUser)
        {
            var invitedUser = _repo.GetUserDetailByEmail(newUser.Email);
            if (invitedUser != null)
            {
                var userRoadTripMap = new PersonToRoadTripMap
                    {
                        RoadTripId = newUser.RoadTripId,
                        UserDetailId = invitedUser.Id
                    };
                if (_repo.AddNewRoadTripUserMap(userRoadTripMap) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, userRoadTripMap);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var invitation = new Invitation
                {
                    GuestEmail = newUser.Email,
                    InvitationStatus = InvitationStatus.Invited,
                    InvitingUserDetailId = newUser.OwnerId
                };
            if (_repo.InviteUser(invitation) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        
    }
}
