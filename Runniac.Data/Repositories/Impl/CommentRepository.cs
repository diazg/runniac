using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories.Impl
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {

        public IEnumerable<Comment> GetByEvent(long eventId)
        {
            return base.Get(
                filter: c => c.Event.Id == eventId,
                orderBy: q => q.OrderBy(e => e.CommentDate));
        }

        public IEnumerable<Comment> GetByUser(int userId)
        {
            return base.Get(filter: c => c.UserId == userId);
        }
    }
}
