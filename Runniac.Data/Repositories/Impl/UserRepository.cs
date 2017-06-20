using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories.Impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        /// </inheritDoc>
        public User GetById(long userId)
        {
            return base.GetByID(userId);
        }

        /// </inheritDoc>
        public User GetByUserName(string userName)
        {
            return base.Get(filter: u => u.UserName == userName).FirstOrDefault();
        }
    }
}
