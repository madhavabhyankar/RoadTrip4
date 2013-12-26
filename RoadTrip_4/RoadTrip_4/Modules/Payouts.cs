using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoadTrip_4.Data;
using RoadTrip_4.Models;

namespace RoadTrip_4.Modules
{
    class Settlement
    {
        public UserDetail UserDetail { get; set; }
        public double SettlementAmount { get; set; }
    }
    
    public class Payouts : IPayouts
    {
        // UserName and Amount
        public ICollection<PayoutModel> CalculatedPayoutsForRoadTrip(int userId, RoadTrip roadTrip)
        {
            //Get all expenses
            var exp = roadTrip.ExpensesForRoadTrip;
            var totalMoneySpent = exp.Where(x => !x.BorrowerId.HasValue).Sum(x => x.Amount);
            if (roadTrip.UserInRoadTrip.Count == 1)
            {
                return new List<PayoutModel>
                    {
                        new PayoutModel
                            {
                                Amount = 0,
                                PayToName = "",
                                PayToUserId = 0
                            }
                    };
            }
            var sharePerUser = Math.Round(totalMoneySpent/roadTrip.UserInRoadTrip.Count, 2);

            
            var settlementQueue = new List<Settlement>();
            foreach (var user in roadTrip.UserInRoadTrip)
            {
                

                // total exp for this user
                var expForUser = roadTrip.ExpensesForRoadTrip.Where(x => x.OwnerId == user.UserDetailId && !x.BorrowerId.HasValue)
                        .Sum(x => x.Amount);
                var moneyBorrowedFromUser =
                    roadTrip.ExpensesForRoadTrip.Where(x => x.OwnerId == user.UserDetailId && x.BorrowerId.HasValue)
                            .Sum(x => x.Amount);
                var moneyBorrowedByUser =
                    roadTrip.ExpensesForRoadTrip.Where(x => x.BorrowerId == user.UserDetailId).Sum(x => x.Amount);

                var moneyFlow = expForUser + moneyBorrowedFromUser - moneyBorrowedByUser - sharePerUser;
                settlementQueue.Add(new Settlement
                    {
                        SettlementAmount = moneyFlow,
                        UserDetail = user.UserDetail
                    });



            }
            var positiveSettlements = new Queue<Settlement>(
                settlementQueue.OrderByDescending(x => x.SettlementAmount).Where(x => x.SettlementAmount > 0));
            var negativeSettlements = new Queue<Settlement>(settlementQueue.OrderByDescending(x => x.SettlementAmount)
                                                                           .Where(x => x.SettlementAmount < 0));

            var SettlementDict = new Dictionary<int, List<PayoutModel>>();
            while (positiveSettlements.Any() && positiveSettlements.Peek() != null)
            {

                var posSet = positiveSettlements.Dequeue();
                while (posSet.SettlementAmount > 0)
                {
                    var negSet = negativeSettlements.Dequeue();
                    if (posSet.SettlementAmount > negSet.SettlementAmount)
                    {
                        AddSettlements(SettlementDict, posSet, negSet, negSet.SettlementAmount);
                        posSet.SettlementAmount -= Math.Abs( negSet.SettlementAmount);

                    }
                    else
                    {
                        while (negSet.SettlementAmount > 0)
                        {
                            AddSettlements(SettlementDict, posSet, negSet, posSet.SettlementAmount);
                            negSet.SettlementAmount += posSet.SettlementAmount;
                            posSet = positiveSettlements.Dequeue();
                        }
                    }

                }
            }
            return SettlementDict[userId];


        }

        private void AddSettlements(Dictionary<int, List<PayoutModel>> SettlementDict, Settlement posSet, Settlement negSet, double settlementDollar)
        {
            if (SettlementDict.ContainsKey(posSet.UserDetail.UserProfileId))
            {
                SettlementDict[posSet.UserDetail.UserProfileId].Add(new PayoutModel
                {
                    Amount = settlementDollar,
                    PayToName = negSet.UserDetail.NickName,
                    PayToUserId = negSet.UserDetail.UserProfileId
                });
            }
            else
            {
                SettlementDict.Add(posSet.UserDetail.UserProfileId, new List<PayoutModel>());
                SettlementDict[posSet.UserDetail.UserProfileId].Add(new PayoutModel
                {
                    Amount = settlementDollar,
                    PayToName = negSet.UserDetail.NickName,
                    PayToUserId = negSet.UserDetail.UserProfileId
                });
            }

            if (SettlementDict.ContainsKey(negSet.UserDetail.UserProfileId))
            {
                SettlementDict[negSet.UserDetail.UserProfileId].Add(new PayoutModel
                {
                    Amount = -1 * settlementDollar,
                    PayToName = posSet.UserDetail.NickName,
                    PayToUserId = posSet.UserDetail.UserProfileId
                });
            }
            else
            {
                SettlementDict.Add(negSet.UserDetail.UserProfileId, new List<PayoutModel>());
                SettlementDict[negSet.UserDetail.UserProfileId].Add(new PayoutModel
                {
                    Amount = -1 * settlementDollar,
                    PayToName = posSet.UserDetail.NickName,
                    PayToUserId = posSet.UserDetail.UserProfileId
                });
            }
        }


    }
}