using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.ExternalDataExtraction
{

    public class EventsMultiSourceExtractor : IMultiExtractor
    {
        private IList<IEventsExtractor> _strategies;

        public EventsMultiSourceExtractor()
        {
            _strategies = new List<IEventsExtractor>();
        }

        /// <inheritDoc/>
        public void AddStrategy(IEventsExtractor strategy)
        {
            _strategies.Add(strategy);
        }

        /// <inheritDoc/>
        public IEnumerable<Event> GetEvents(string extractor)
        {
            var events = new List<Event>();

            BuildExtractor(extractor);

            foreach (var item in _strategies)
                events.AddRange(item.GetEvents());

            return events.OrderBy(e => e.EventDate);
        }

        /// <inheritDoc/>
        public Event GetExtraInfo(Event eventObj, string extractor)
        {
            BuildExtractor(extractor);

            if (_strategies != null && _strategies.Count > 0)
                return _strategies.First().GetExtraInfo(eventObj);

            return null;
        }

        /// <summary>
        /// Asigna las estrategias apropiadas al extractor de eventos en función del string recibido.
        /// </summary>
        /// <param name="extractors">Cadena de texto con el nombre de las fuentes a utilizar para extraer
        /// información separadas por el carácter '~'.</param>
        private void BuildExtractor(string extractor)
        {
            if (extractor == "fedNav")
            {
                this.AddStrategy(new FederacionNavarraExtractor());
            }
            if (extractor == "todoCarreras")
            {
                this.AddStrategy(new TodoCarrerasExtractor());
            }
            if (extractor == "vamosACorrer")
            {
                this.AddStrategy(new VamosACorrerExtractor());
            }
        }
    }
}
