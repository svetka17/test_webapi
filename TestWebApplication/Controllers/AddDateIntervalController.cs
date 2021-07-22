using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class DateIntervalsController : ApiController
    {

        public class DateInterval
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }


        [Route("api/dateintervals/select")]
        [HttpGet]
        public List<DateInterval> Select([FromUri] long ticksFrom, [FromUri] long ticksTo)
        {
  
            DateTime dateFrom = new DateTime(ticksFrom);
            DateTime dateTo = new DateTime(ticksTo);
            List<DateInterval> query;
            using (DateIntervalContext context = new DateIntervalContext())
            {
                query = context.DateIntervals
                    .Where(di => (di.DateFrom >= dateFrom && di.DateTo <= dateFrom) || (di.DateTo >= dateTo && di.DateFrom <= dateTo))
                    .Select(di => new DateInterval { DateFrom = di.DateFrom, DateTo = di.DateTo})
                    .OrderBy(di => di)
                    .ToList();                
            }
            return query;
        }

        [Route("api/dateintervals/insert")]
        [HttpPost]
        public void Insert([FromBody]DateInterval values)
        {
            using (DateIntervalContext context = new DateIntervalContext())
            {
                var dates = new Models.DateInterval
                {
                    DateFrom = values.DateFrom,
                    DateTo = values.DateTo
                };

                context.DateIntervals.Add(dates);
                context.SaveChanges();
            }
        }

    }
}