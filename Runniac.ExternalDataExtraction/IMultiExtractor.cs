using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.ExternalDataExtraction
{
    public interface IMultiExtractor
    {
        /// <summary>
        /// Añade una estrategia a la extracción de eventos. Una estrategia es un sitio web
        /// del que vamos a intentar extraer eventos.
        /// </summary>
        /// <param name="strategy">Estrategia a utilizar.</param>
        void AddStrategy(IEventsExtractor strategy);

        /// <summary>
        /// Retorna todos los eventos de las distintas páginas web configuradas.
        /// </summary>
        /// <returns>La lista de eventos encontrados.</returns>
        IEnumerable<Event> GetEvents(string extractor);

        /// <summary>
        /// Completa la información de un evento. Este método es útil para intentar encontrar
        /// información adicional en la página de detalle del evento.
        /// </summary>
        /// <param name="eventObj">Evento del que se va a intentar completar su información.</param>
        /// <returns></returns>
        Event GetExtraInfo(Event eventObj, string extractor);
    }
}
