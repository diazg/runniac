using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        /// <summary>
        /// Retorna todas las fotos encontradas en base de datos para un determinado evento.
        /// </summary>
        /// <param name="eventId">El ID del evento.</param>
        /// <returns>Una colección de fotos.</returns>
        IEnumerable<Photo> GetByEvent(long eventId);

        /// <summary>
        /// Retorna todas las fotos encontradas en base de datos para un determinado usuario.
        /// </summary>
        /// <param name="userId">El ID del usuario.</param>
        /// <returns>Una colección de fotos.</returns>
        IEnumerable<Photo> GetByUser(int userId);
    }
}
