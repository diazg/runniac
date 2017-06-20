using Runniac.Data;
using Runniac.Data.Repositories;
using Runniac.ExternalDataExtraction;
using Runniac.Utils;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business.Impl
{
    public class PhotoService : AbstractService, IPhotoService
    {
        public PhotoService(IUnitOfWork uow)
            : base(uow)
        { }

        /// <inheritdoc />
        public void SavePhoto(Photo photo)
        {            
            _uow.PhotoRepository.Insert(photo);
            _uow.Save();
        }

        /// <inheritdoc />
        public IEnumerable<Photo> GetAllPhotosByEvent(long eventId)
        {
            var photos = _uow.PhotoRepository.GetByEvent(eventId);

            return photos;
        }

        /// <inheritdoc />
        public IEnumerable<Photo> GetPhotosByUser(int userId)
        {
            return _uow.PhotoRepository.GetByUser(userId);
        }
    }
}
