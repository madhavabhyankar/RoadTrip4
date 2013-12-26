using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoadTrip_4.Data;

namespace RoadTrip_4.Controllers
{
    public class RoadTripsController : ApiController
    {
        private readonly IRoadTripRepo _repo;

        public RoadTripsController(IRoadTripRepo repo)
        {
            _repo = repo;
        }
        public IEnumerable<RoadTrip> Get(int id)
        {
            return _repo.GetRoadTripsForUser(id);

        }  

        [HttpPost]
        public HttpResponseMessage Post([FromBody] RoadTrip roadTrip)
        {
            roadTrip.Id = Guid.NewGuid();
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
    }
}
