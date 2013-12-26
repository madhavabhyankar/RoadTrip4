using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RoadTrip_4.Data;

namespace RoadTrip_4.Controllers
{
    public class ExpensesController : ApiController
    {
        private readonly IRoadTripRepo _repo;

        public ExpensesController(IRoadTripRepo repo)
        {
            _repo = repo;
        }

       
        // GET api/expenses/5
        public IEnumerable<Expense> Get(Guid id)
        {
            return _repo.GetExpensesForRoadTrip(id);
        }

        public IEnumerable<Expense> Get(Guid roadTripId, int userId)
        {
            return _repo.GetExpensesForRoadTrip(roadTripId).Where(x => x.OwnerId == userId);
        }
        // POST api/expenses
        public HttpResponseMessage Post([FromBody]Expense value)
        {
            if (_repo.AddNewRoadTripExpense(value) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, value);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/expenses/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/expenses/5
        public void Delete(int id)
        {
        }
    }
}
