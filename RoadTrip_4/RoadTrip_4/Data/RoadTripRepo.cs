using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Data
{
    public class RoadTripRepo : IRoadTripRepo
    {
        private readonly RoadTripContext _context;

        public RoadTripRepo(RoadTripContext context)
        {
            _context = context;
        }

        public void AddUserDetail(string username, string FirstName, string LastName, string NickName, string Email)
        {
            var userProfile = _context.UserProfiles.First(x => x.UserName == username);
             _context.UserDetails.Add(new UserDetail
                {
                    UserProfileId = userProfile.UserId,
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    NickName = NickName

                });
            
            
        }

        public UserDetail GetUserDetailForAspNetLogin(string userName)
        {
            return _context.UserDetails.Include("UserProfile").First(x => x.UserProfile.UserName == userName);
        }

        public IQueryable<RoadTrip> GetRoadTripsCreatedByUser(int userDetailId)
        {
            return _context.RoadTrips.Where(x => x.OwnerId == userDetailId);
        }

        public IQueryable<RoadTrip> GetRoadTripsForUser(int userDetailId)
        {
            return
                _context.PersonToRoadTripMaps.Include("RoadTrip")
                        .Where(x => x.UserDetailId == userDetailId)
                        .Select(x => x.RoadTrip);
        }

        public IQueryable<UserDetail> GetUsersForRoadTrip(Guid roadTripId)
        {
            return _context.PersonToRoadTripMaps.Include("UserDetail").Where(x => x.RoadTripId == roadTripId)
                           .Select(x => x.UserDetail);
        }

        public IQueryable<Expense> GetExpensesForRoadTrip(Guid roadTripId)
        {
            return _context.Expenses.Include("Owner").Where(x => x.RoadTripId == roadTripId);
        }

        public RoadTrip GetRoadTripByRoadTripId(Guid roadTripId, bool includeExpenses, bool includeUsers)
        {
            if (includeExpenses && includeUsers)
            {
                return
                    _context.RoadTrips.Include("ExpensesForRoadTrip")
                            .Include("UserInRoadTrip")
                            .Include("UserInRoadTrip.UserDetail")
                            .FirstOrDefault(x => x.Id == roadTripId);
            }
            else
            {
                return _context.RoadTrips.FirstOrDefault(x => x.Id == roadTripId);
            }
        }

        public UserDetail GetUserDetailByEmail(string email)
        {
            return _context.UserDetails.FirstOrDefault(x => x.Email == email);
        }

        public bool InviteUser(Invitation invitation)
        {
            _context.Invitations.Add(invitation);
            return true;
        }

        public bool AddNewRoadTrip(RoadTrip roadTrip)
        {
            try
            {
                _context.RoadTrips.Add(roadTrip);
                return true;
            }
            catch (Exception)
            {
                return false;
                
            }
        }

        public bool AddNewRoadTripUserMap(PersonToRoadTripMap map)
        {
            try
            {
                _context.PersonToRoadTripMaps.Add(map);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool AddNewRoadTripExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            return true;
        }


        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception exception)
            {
                return false;
                throw;
            }
        }
    }
}