using AutoMapper;
using log4net;
using Runniac.Business;
using Runniac.Data;
using Runniac.ExternalDataExtraction;
using Runniac.Membership;
using Runniac.Membership.Models;
using Runniac.Utils;
using Runniac.Web.ViewModels;
using Runniac.Web.WebAppServices;
using Runniac.Web.WebUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;
using WebMatrix.WebData;

namespace Runniac.Web.Controllers
{
    public class PhotosController : ApiController
    {
        private IPhotoService _photoService;
        private IWebSecurityService _securityService;
        private IUsersContext _usersDbContext;
        private ILog _logger = LogManager.GetLogger(typeof(MvcApplication));

        public PhotosController(IPhotoService photoService, IWebSecurityService securityService, IUsersContext usersDbContext)
        {
            _photoService = photoService;
            _securityService = securityService;
            _usersDbContext = usersDbContext;
        }

        // GET api/<controller>/getForEvent        
        ///// <summary>
        ///// Retorna todas las fotos encontradas para un evento.
        ///// </summary>
        ///// <param name="eventId">Id del evento para el que se quieren recuperar las fotos.</param>
        ///// <returns>Una colección de fotos.</returns>
        //[InvalidateCacheOutput("Get", typeof(EventsController))]
        //public IEnumerable<PhotoVM> GetForEvent(long eventId)
        //{
        //    if (eventId <= 0)
        //        throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

        //    var photos = _photoService.GetAllPhotosByEvent(eventId);
        //    var photosVM = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoVM>>(photos);

        //    foreach (var p in photosVM)
        //    {
        //        if (p.User != null && p.User.UserId > 0)
        //        {
        //            var user = _usersDbContext.UserProfiles.Where(u => u.UserId == p.User.UserId).FirstOrDefault();
        //            p.User = Mapper.Map<UserInfo, UserVM>(user);
        //        }
        //    }

        //    return photosVM;
        //}


        // POST api/<controller>/upload
        /// <summary>
        /// Guarda un conjunto de fotos asociadas a un evento.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);

            if (Request.Content.Headers.ContentLength > ImageUtils.MAX_IMAGE_SIZE)
                return this.Request.CreateResponse(HttpStatusCode.BadRequest,
                    "No se permiten subidas de archivos mayores de 1 MB");

            var provider = FileUtils.GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            var file = result.FileData.First();
            var mime = file.Headers.ContentType.MediaType;

            if (ImageUtils.IsImage(mime))
            {
                var name = String.Format("{0}.{1}", Guid.NewGuid().ToString(), ImageUtils.GetExtensionFromMime(mime));
                var photo = new PhotoVM
                {
                    PhotoDate = String.Format("{0:dd/MM/yyyy}", DateTime.Now),
                    UserId = _securityService.CurrentUserId,
                    Url = String.Format("{0}{1}",
                            ConfigurationManager.AppSettings["EventPhotosPath"].ToString(), name),
                    EventId = long.Parse(result.FormData["eventId"])
                };

                FileUtils.SaveFileToDisk(file, name, ConfigurationManager.AppSettings["EventPhotosPath"]);
                _photoService.SavePhoto(Mapper.Map<PhotoVM, Photo>(photo));

                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return this.Request.CreateResponse(HttpStatusCode.BadRequest,
                    "El fichero no es una imagen válida. Sólo se aceptan archivos en formato JPG, GIF, PNG o BMP");
        }
    }
}