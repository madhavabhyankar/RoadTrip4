using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoadTrip_4.Data;
using RoadTrip_4.Modules;

namespace RoadTrip_4.Controllers
{
    public class RoadTripsController : ApiController
    {
        private readonly IRoadTripRepo _repo;
        private readonly IUtilities _utilities;

        public RoadTripsController(IRoadTripRepo repo, IUtilities utilities)
        {
            _repo = repo;
            _utilities = utilities;
        }

        public IEnumerable<RoadTrip> Get(int id)
        {
            return _repo.GetRoadTripsForUser(id);

        }  

        [HttpPost]
        public HttpResponseMessage Post([FromBody] RoadTrip roadTrip)
        {
            roadTrip.Id = Guid.NewGuid();
            roadTrip.RoadTripStatus = RoadTripStatus.Active;
            roadTrip.RoadTripHashId = _utilities.GetUniqueKey(5);
            if (_repo.AddNewRoadTrip(roadTrip) 
                && _repo.AddNewRoadTripUserMap(new PersonToRoadTripMap
                    {
                        RoadTripId = roadTrip.Id,
                        UserDetailId = roadTrip.OwnerId
                    })
                && _repo.Save())
            {
                
                return Request.CreateResponse(HttpStatusCode.Created, roadTrip);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            var roadTrip = _repo.GetRoadTripByRoadTripId(id, false, false);
            roadTrip.RoadTripStatus = RoadTripStatus.Deleted;
            if (_repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, roadTrip);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public RoadTrip GetAndAddUserToRoadTrip(string roadTripId, int userId, string ownerEmailId)
        {
            var roadTrip = _repo.GetRoadTripByRoadTripHash(roadTripId);
            var owner = _repo.GetUserDetailByEmail(ownerEmailId);
            if (roadTrip.OwnerId == userId)
            {
                throw new Exception("You are the owner of this road trip!");
            }
            if (owner == null)
            {
                throw new Exception("Owner by that email not found!");
            }
            if (roadTrip == null)
            {
                throw new Exception(String.Format("Road trip for id:{0} was not found", roadTrip));
            }
            if (roadTrip.OwnerId != owner.Id)
            {
                throw new Exception(String.Format("Road trip for Id: {0} does not belong to {1}",roadTripId,ownerEmailId));
            }
            
                if (_repo.AddNewRoadTripUserMap(new PersonToRoadTripMap
                    {
                        RoadTripId = roadTrip.Id,
                        UserDetailId = userId
                    })
                    && _repo.Save())
                {
                    return roadTrip;
                }
            throw new Exception("Something bad happend!");
        }
    }
}
