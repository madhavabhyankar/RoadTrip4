using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoadTrip_4.Data;
using RoadTrip_4.Models;
using RoadTrip_4.Modules;

namespace RoadTrip_4.Controllers
{
    public class PayoutController : ApiController
    {
        private readonly IRoadTripRepo _repo;
        private readonly IPayouts _payouts;

        public PayoutController(IRoadTripRepo repo, IPayouts payouts)
        {
            _repo = repo;
            _payouts = payouts;
        }

        // GET api/payout/5
        public IEnumerable<PayoutModel> Get(Guid roadTripId, int userId)
        {
            var roadTrip = _repo.GetRoadTripByRoadTripId(roadTripId, true, true);
            var payouts = _payouts.CalculatedPayoutsForRoadTrip(userId, roadTrip);
            return payouts;
        }

        
    }
}
