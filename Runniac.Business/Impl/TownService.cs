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
    public class TownService : AbstractService, ITownService
    {
        public TownService(IUnitOfWork uow)
            : base(uow)
        { }


        /// <inheritdoc />
        public IEnumerable<string> Search(string query)
        {
            return _uow.TownRepository.Search(query);
        }

        /// <inheritdoc />
        public Town GetByName(string name)
        {
            return _uow.TownRepository.GetByName(name);
        }
    }
}
