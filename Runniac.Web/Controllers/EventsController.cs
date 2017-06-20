using AutoMapper;
using log4net;
using Runniac.Business;
using Runniac.Data;
using Runniac.ExternalDataExtraction;
using Runniac.Utils;
using Runniac.Web.ViewModels;
using Runniac.Web.WebUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace Runniac.Web.Controllers
{
    public class EventsController : ApiController
    {
        private IEventService _eventService;
        private ICommentService _commentService;
        private ITownService _townService;
        private IUserService _userService;
        private IMultiExtractor _eventsExtractor;
        private ILog _logger = LogManager.GetLogger(typeof(MvcApplication));

        public EventsController(IEventService eventService, ICommentService commentService, ITownService townService,
            IUserService userService, IMultiExtractor eventsExtractor)
        {
            _eventService = eventService;
            _commentService = commentService;
            _eventsExtractor = eventsExtractor;
            _townService = townService;
            _userService = userService;
        }


        // GET api/<controller>/getExternal
        [System.Web.Mvc.RequireHttps]
        [ActionName("getExternal")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<EventVM> GetEventsFromExternalSource(string extractor)
        {
            if (String.IsNullOrEmpty(extractor))
            {
                _logger.Debug(String.Format("Extractor es null. Extractors: {0}", extractor));
                return null;
            }

            var events = _eventsExtractor.GetEvents(extractor);
            return Mapper.Map<IEnumerable<Event>, IEnumerable<EventVM>>(events);
        }

        // GET api/<controller>/search
        [HttpGet]
        [ActionName("search")]
        [CacheOutput(ClientTimeSpan = 120, ServerTimeSpan = 120)]
        public IEnumerable<EventVM> SearchEvents(string location = null, string eventType = null, string eventDate = null)
        {
            var events = _eventService.SearchEvents(location, eventType, eventDate);
            var eventsToReturn = Mapper.Map<IEnumerable<Event>, IEnumerable<EventVM>>(events);

            foreach (var e in eventsToReturn)
            {
                e.Town = Mapper.Map<Town, TownVM>(_townService.GetByName(e.Location));
                e.AvgRating = _eventService.GetRating(Mapper.Map<EventVM, Event>(e));
            }

            return eventsToReturn;
        }

        // GET api/<controller>/getDifferentLocations
        [HttpGet]
        [ActionName("getDifferentLocations")]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public IEnumerable<string> GetDifferentLocations(string query)
        {
            var locations = _eventService.GetLocations(query);
            return locations;
        }

        // POST api/<controller>/import
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        [ActionName("import")]
        public void ImportEvents(ImportEventsVM importInfo)
        {
            if (importInfo == null)
                return;

            var eventsList = Mapper.Map<IEnumerable<EventVM>, IEnumerable<Event>>(importInfo.Events);
            foreach (var e in eventsList)
            {
                var ev = _eventsExtractor.GetExtraInfo(e, importInfo.Extractor);
                _eventService.SaveEvent(ev);
            }
        }

        // GET api/<controller>/5
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public EventVM Get(long id)
        {
            var race = _eventService.GetEventById(id);
            var raceVm = Mapper.Map<Event, EventVM>(race);

            foreach (var item in raceVm.Comments)
            {
                item.User.Points = _userService.GetPoints(item.UserId);
            }

            return raceVm;
        }

        // GET api/<controller>
        public IEnumerable<EventVM> Get()
        {
            var events = _eventService.GetAllEvents();
            return Mapper.Map<IEnumerable<Event>, IEnumerable<EventVM>>(events);
        }

        // GET api/<controller>/GetClosest
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<EventVM> GetClosest(decimal lat, decimal lon)
        {
            var races = _eventService.GetClosest(lat, lon);
            return Mapper.Map<IEnumerable<Event>, IEnumerable<EventVM>>(races);
        }

        // GET api/<controller>/GetTopRated
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<EventVM> GetTopRated()
        {
            var races = _eventService.GetTopRated();
            return Mapper.Map<IEnumerable<Event>, IEnumerable<EventVM>>(races);
        }

        // GET api/<controller>/link
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        [ActionName("getExtraInfo")]
        public EventVM GetExtraInfo([FromBody]ExtraInfoVM extraInfo)
        {
            if (extraInfo == null)
                return null;

            var eventInfo = _eventsExtractor.GetExtraInfo(Mapper.Map<EventVM, Event>(extraInfo.Event), extraInfo.Extractor);
            return Mapper.Map<Event, EventVM>(eventInfo);
        }

        // POST api/<controller>/saveEvent
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        [ActionName("saveEvent")]
        [InvalidateCacheOutput("SearchEvents")]
        public dynamic PostEvent(EventVM race)
        {
            if (race != null && ModelState.IsValid)
            {
                var eventModel = Mapper.Map<EventVM, Event>(race);

                if (eventModel.Id == 0)
                    _eventService.SaveEvent(eventModel);
                else
                    _eventService.UpdateEvent(eventModel);

                return new { Id = eventModel.Id };
            }
            else
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
        }

        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        [InvalidateCacheOutput("SearchEvents")]
        [InvalidateCacheOutput("Get")]
        public void Update(EventVM race)
        {
            if (race != null && ModelState.IsValid)
            {
                var eventModel = Mapper.Map<EventVM, Event>(race);
                _eventService.UpdateEvent(eventModel);
            }
            else
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
        }

        // DELETE api/<controller>/5
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [InvalidateCacheOutput("SearchEvents")]
        public void Delete(long id)
        {
            _eventService.DeleteEvent(id);
        }

        // POST api/<controller>/upload
        /// <summary>
        /// Guarda un conjunto de fotos asociadas a un evento.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadCourse()
        {
            if (!Request.Content.IsMimeMultipartContent())
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

            if (Request.Content.Headers.ContentLength > GisUtils.MAX_IMAGE_SIZE)
                return this.Request.CreateResponse(HttpStatusCode.BadRequest,
                    "No se permiten subidas de archivos mayores de 5 MB");

            var provider = FileUtils.GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            var file = result.FileData.First();
            var extension = Path.GetExtension(StringUtils.RemoveSpecialCharacters(file.Headers.ContentDisposition.FileName));

            if (GisUtils.IsGisFile(extension))
            {
                var name = String.Format("{0}.{1}", Guid.NewGuid().ToString(), extension);
                var eventObj = _eventService.GetEventById(long.Parse(result.FormData["eventId"]));
                eventObj.CourseFileName = name;

                FileUtils.SaveFileToDisk(file, name, ConfigurationManager.AppSettings["CourseFiles"]);
                _eventService.UpdateEvent(eventObj);

                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return this.Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Extensión no válida. Sólo se aceptan archivos en formato KML y GPX");
        }



    }
}