using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories.Impl
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {

   
        public IEnumerable<Photo> GetByEvent(long eventId)
        {
            return base.Get(filter: p => p.Event.Id ==eventId);            
        }

        public IEnumerable<Photo> GetByUser(int userId)
        {
            return base.Get(filter: p => p.UserId == userId);
        }
    }
}
