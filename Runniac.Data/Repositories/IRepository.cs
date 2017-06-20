using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        MyDatabaseContext Context { get; set; }

        void Insert(T entity);

        void Update(T entityToUpdate);

        void Delete(object id);

        void Delete(T entityToDelete);

        T GetByID(object id);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int limitResults = 0);
    }
}
