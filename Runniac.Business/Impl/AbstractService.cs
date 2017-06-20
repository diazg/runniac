using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business.Impl
{
    public class AbstractService
    {
        protected IUnitOfWork _uow;

        public AbstractService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
