using System;
using System.Linq;

namespace RoadTrip_4.Data
{
    public interface IRoadTripRepo
    {
        void AddUserDetail(string username, string FirstName, string LastName, string NickName, string Email);
        UserDetail GetUserDetailForAspNetLogin(string userName);
        IQueryable<RoadTrip> GetRoadTripsCreatedByUser(int userDetailId);
        IQueryable<RoadTrip> GetRoadTripsForUser(int userDetailId);
        IQueryable<UserDetail> GetUsersForRoadTrip(Guid roadTripId);
        IQueryable<Expense> GetExpensesForRoadTrip(Guid roadTripId);
        RoadTrip GetRoadTripByRoadTripId(Guid roadTripId, bool includeExpenses, bool includeUsers);
        RoadTrip GetRoadTripByRoadTripHash(string roadTripHashId);
        UserDetail GetUserDetailByEmail(string email);
        bool InviteUser(Invitation invitation);
        bool AddNewRoadTrip(RoadTrip roadTrip);
        bool AddNewRoadTripUserMap(PersonToRoadTripMap map);
        bool AddNewRoadTripExpense(Expense expense);
        bool Save();
    }
}