using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business
{
    public interface ITownService
    {
        /// <summary>
        /// Retorna todos los municipios diferentes de España.
        /// </summary>
        /// <returns>Lista de municipios.</returns>
        IEnumerable<string> Search(string query);

        /// <summary>
        /// Retorna un municipio dado su nombre.
        /// </summary>
        /// <param name="name">Nombre del municipio a buscar.</param>
        /// <returns>El municipio buscado.</returns>
        Town GetByName(string name);
    }
}
