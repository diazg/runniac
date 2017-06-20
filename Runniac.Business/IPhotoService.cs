using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business
{
    public interface IPhotoService
    {
        /// <summary>
        /// Retorna todas las fotos encontradas para un determinado evento.
        /// </summary>
        /// <returns>Una colección de fotos.</returns>
        IEnumerable<Photo> GetAllPhotosByEvent(long eventId);

        /// <summary>
        /// Guarda una lista de fotos.
        /// </summary>
        /// <param name="photos">Lista de fotos a guardar.</param>
        void SavePhoto(Photo photo);

        /// <summary>
        /// Retorna las fotos subidas por un usuario.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <returns>La lista de fotos subidas por el usuario.</returns>
        IEnumerable<Photo> GetPhotosByUser(int userId);
    }
}
