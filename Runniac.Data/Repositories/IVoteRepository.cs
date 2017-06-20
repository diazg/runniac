using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories
{
    public interface IVoteRepository : IRepository<Vote>
    {
        /// <summary>
        /// Retorna todos los votos recibidos para un evento concreto.
        /// </summary>
        /// <returns>Una colección de votos.</returns>
        IEnumerable<Vote> GetByComment(long commentId);

        /// <summary>
        /// Busca un voto en base de datos a partir de un comentario y un usuario.
        /// </summary>
        /// <param name=param name="commentId">ID del comentario.</param>
        /// <param name="userId">ID del usuario que ha hecho el comentario.</param>
        /// <returns>El voto encontrado.</returns>
        Vote Find(long commentId, int userId);
    }
}
