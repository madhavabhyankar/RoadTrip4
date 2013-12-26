using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoadTrip_4.Data;
using RoadTrip_4.Models;

namespace RoadTrip_4.Controllers
{
    public class MyRoadTripsController : Controller
    {
        private readonly IRoadTripRepo _roadTripRepo;

        public MyRoadTripsController(IRoadTripRepo roadTripRepo)
        {
            _roadTripRepo = roadTripRepo;
        }

        //
        // GET: /MyRoadTrips/
        [Authorize]
        public ActionResult Index()
        {
            var user = _roadTripRepo.GetUserDetailForAspNetLogin(User.Identity.Name);
            return View(user);
        }

    }
}
