using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Retorna un usuario por su ID.
        /// </summary>
        /// <returns>El usuario buscado.</returns>
        User GetById(long userId);


        User GetByUserName(string userName);
    }
}
