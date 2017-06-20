using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business
{
    public interface IUserService
    {
        /// <summary>
        /// Retorna los puntos de un usuario. Para calcular los puntos se tienen en cuenta:
        /// - Votos recibidos por sus comentarios.
        /// - Fotos subidas y publicadas en el sitio Web.
        /// </summary>
        /// <returns>La puntuación de un usuario.</returns>
        int GetPoints(int userId);

        /// <summary>
        /// Retorna un usuario por su ID.
        /// </summary>
        /// <returns>El usuario buscado.</returns>
        User GetById(long userId);

        /// <summary>
        /// Retorna un usuario por su nombre de usuario único.
        /// </summary>
        /// <param name="userName">Nombre de usuario.</param>
        /// <returns>El usuario encontrado.</returns>
        User GetByUserName(string userName);

        /// <summary>
        /// Guarda un usuario.
        /// </summary>
        /// <param name="user">Usuario a guardar.</param>
        void SaveUser(User user);
    }
}
