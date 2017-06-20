using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        /// <summary>
        /// Retorna todos los comentarios encontrados en base de datos para un determinado evento.
        /// </summary>
        /// <returns>Una colección de comentarios.</returns>
        IEnumerable<Comment> GetByEvent(long eventId);


        /// <summary>
        /// Retorna todos los comentarios encontrados en base de datos para un determinado usuario.
        /// </summary>
        /// <returns>Una colección de comentarios.</returns>
        IEnumerable<Comment> GetByUser(int userId);
    }
}
