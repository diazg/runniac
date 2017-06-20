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
    public class TownRepository : GenericRepository<Town>, ITownRepository
    {

        /// <inheritDoc/>
        public IEnumerable<string> Search(string query)
        {
            return base.Get(filter: t => t.Name.Contains(query)).Select(t => t.Name);
        }

        /// <inheritDoc/>
        public Town GetByName(string name)
        {
            return base.Get(filter: t => t.Name == name || t.Name.StartsWith(name + "/")).FirstOrDefault();
        }
    }
}
