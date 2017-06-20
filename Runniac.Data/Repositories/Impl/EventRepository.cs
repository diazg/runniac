using Runniac.Data.Utils;
using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories.Impl
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private const int RESULTS_NUMBER = 6;

        /// <inheritDoc/>
        public IEnumerable<Event> Find(string location, string eventType, DateTime? eventDate)
        {
            var predicate = PredicateBuilder.True<Runniac.Data.Event>();
            predicate = predicate.And(e => e.EventDate >= DateTime.Today && e.Published);

            if (!String.IsNullOrEmpty(location))
                predicate = predicate.And(e => e.Location.ToLower() == location.ToLower());

            if (!String.IsNullOrEmpty(eventType))
                predicate = predicate.And(e => e.Type == eventType);

            if (eventDate != null)
                predicate = predicate.And(e => e.EventDate <= eventDate);

            return base.Get(filter: predicate, orderBy: q => q.OrderBy(e => e.EventDate), includeProperties: "Comments");
        }

        /// <inheritDoc/>
        public IEnumerable<string> GetLocations(string query)
        {
            return base.Get(filter: e => e.EventDate >= DateTime.Today && e.Published && e.Location.Contains(query)).
                Select(e => e.Location).Distinct();
        }

        /// <inheritDoc/>
        public IEnumerable<Event> GetAllOrdered()
        {
            return base.Get(orderBy: q => q.OrderBy(e => e.EventDate));
        }

        /// <inheritDoc/>
        public IEnumerable<Event> GetClosest(decimal lat, decimal lon)
        {
            var buffer = new StringBuilder();
            buffer.Append("SELECT TOP ");
            buffer.Append(RESULTS_NUMBER);
            buffer.Append(" e.*, 3956 * 2 * ASIN(SQRT( POWER(SIN((@lat - abs(t.Latitude)) * pi()/180 / 2), 2)");
            buffer.Append(" + COS(@lon * pi()/180 ) * COS(abs(t.Latitude) * pi()/180)");
            buffer.Append("* POWER(SIN((@lon - t.Longitude) * pi()/180 / 2), 2) )) AS DistanceFromUser ");
            buffer.Append("FROM Events e, Towns t WHERE (t.Name = e.Location OR t.Name LIKE e.Location + '/%') AND e.Published=1 AND ");
            buffer.Append("e.EventDate >= GETDATE() ORDER BY DistanceFromUser");

            var args = new DbParameter[] { 
                new SqlParameter { ParameterName = "lat", Value = lat },
                new SqlParameter { ParameterName = "lon", Value = lon }
            };
            var events = Context.Database.SqlQuery<Event>(buffer.ToString(), args).ToList();

            return events;
        }

        /// <inheritDoc/>
        public IEnumerable<Event> GetTopRated()
        {
            var buffer = new StringBuilder();
            buffer.Append("SELECT TOP ");
            buffer.Append(RESULTS_NUMBER);
            buffer.Append(" e.*, s.RatingAvg FROM Events e LEFT JOIN ");
            buffer.Append("(SELECT EventId, Avg(Rating) AS RatingAvg FROM Comments GROUP BY EventId) s ");
            buffer.Append("ON e.Id = s.EventId WHERE e.Published=1 AND ");
            buffer.Append("e.EventDate >= GETDATE() ORDER BY RatingAvg DESC;");

            var events = Context.Database.SqlQuery<Event>(buffer.ToString()).ToList();

            return events;
        }
    }
}
