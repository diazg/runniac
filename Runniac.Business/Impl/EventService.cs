using Runniac.Data;
using Runniac.Data.Repositories;
using Runniac.ExternalDataExtraction;
using Runniac.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business.Impl
{
    public class EventService : AbstractService, IEventService
    {
        private ICommentService _commentService;

        public EventService(IUnitOfWork uow, ICommentService commentService)
            : base(uow)
        {
            _commentService = commentService;
        }

        /// <inheritdoc />
        public void SaveEvent(Event eventObj)
        {
            _uow.EventRepository.Insert(eventObj);
            _uow.Save();
        }

        /// <inheritdoc />
        public IEnumerable<Event> GetAllEvents()
        {
            return _uow.EventRepository.GetAllOrdered();
        }

        /// <inheritdoc />
        public IEnumerable<Event> SearchEvents(string location, string eventType, string eventDate)
        {
            var parseDate = ParseUtils.ParseDate(eventDate);
            return _uow.EventRepository.Find(location, eventType, parseDate);
        }

        /// <inheritdoc />
        public IEnumerable<string> GetLocations(string query)
        {
            return _uow.EventRepository.GetLocations(query);
        }

        /// <inheritdoc />
        public Event GetEventById(long id)
        {
            var race = _uow.EventRepository.Get(
                filter: e => e.Id == id,
                includeProperties: "Comments.User, Comments.Votes, Photos.User",
                limitResults: 1).FirstOrDefault();

            if (race.Comments != null)
            {
                foreach (var item in race.Comments)
                {
                    foreach (var vote in item.Votes)
                    {
                        item.Ranking += (vote.Positive ? 1 : -1);
                    }
                }
            }

            if (race != null)
                race.AvgRating = GetRating(race);

            return race;
        }

        /// <inheritdoc />
        public void DeleteEvent(long id)
        {
            _uow.EventRepository.Delete(id);
            _uow.Save();
        }

        /// <inheritdoc />
        public void UpdateEvent(Event eventModel)
        {
            _uow.EventRepository.Update(eventModel);
            _uow.Save();
        }

        /// <inheritdoc />
        public IEnumerable<Event> GetClosest(decimal lat, decimal lon)
        {
            return _uow.EventRepository.GetClosest(lat, lon);
        }

        /// <inheritdoc />
        public IEnumerable<Event> GetTopRated()
        {
            return _uow.EventRepository.GetTopRated();
        }

        /// <inheritdoc />
        public float GetRating(Event e)
        {
            if (e.Comments != null && e.Comments.Count() > 0)
                return e.Comments.Sum(c => c.Rating) / e.Comments.Count();

            return 0;
        }
    }
}
