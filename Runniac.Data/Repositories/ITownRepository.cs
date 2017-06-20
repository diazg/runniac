using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data.Repositories
{
    public interface ITownRepository : IRepository<Town>
    {
        /// <summary>
        /// Retorna todos los municipios existentes.
        /// </summary>
        /// <returns>Lista de localidades.</returns>
        IEnumerable<string> Search(string query);

        /// <summary>
        /// Obtiene un municipio a partir de su nombre.
        /// </summary>
        /// <param name="name">Nombre del municipio a buscar.</param>
        /// <returns></returns>
        Town GetByName(string name);
    }
}
