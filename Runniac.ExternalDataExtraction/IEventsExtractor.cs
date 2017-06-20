using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.ExternalDataExtraction
{
    public interface IEventsExtractor
    {
        /// <summary>
        /// Retorna una lista de eventos leída de una web externa inspeccionando su HTML.
        /// </summary>
        /// <returns>La lista de eventos.</returns>
        IEnumerable<Event> GetEvents();

        /// <summary>
        /// Obtiene toda la información disponible para un evento en una web externa. A
        /// menudo las páginas facilitan un listado del que se nutre el método GetEvents(),
        /// para luego tener una página de detalle de cada carrera en el que se puede
        /// obtener más información, y que es la que inspecciona este método.
        /// </summary>
        /// <param name="url">URL a la págia del evento del que se quiere obtener toda la 
        /// información posible.</param>
        /// <returns>El evento.</returns>
        Event GetExtraInfo(Event eventObj);
    }
}
