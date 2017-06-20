using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories.Impl
{
    public class VoteRepository: GenericRepository<Vote>, IVoteRepository
    {
    
        public IEnumerable<Vote> GetByComment(long commentId)
        {
            return base.Get(filter: v => v.Comment.Id == commentId);
        }

        public Vote Find(long commentId, int userId)
        {
            return base.Get(
                filter: c => c.Comment.Id == commentId && c.UserId == userId).FirstOrDefault();
        }
    }
}
