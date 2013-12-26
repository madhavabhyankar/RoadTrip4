using System.Collections.Generic;
using RoadTrip_4.Data;
using RoadTrip_4.Models;

namespace RoadTrip_4.Modules
{
    public interface IPayouts
    {
        ICollection<PayoutModel> CalculatedPayoutsForRoadTrip(int userId, RoadTrip roadTrip);
    }
}