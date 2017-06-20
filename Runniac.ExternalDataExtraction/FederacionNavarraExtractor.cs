using HtmlAgilityPack;
using Runniac.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.ExternalDataExtraction
{

    public class FederacionNavarraExtractor : IEventsExtractor
    {
        private const string FED_NAVARRA_BASE_URL = "http://www.fnaf.es/nweb/";
        private const string CALENDAR_URL = "usr_cal.php";

        /// <inheritDoc/>
        public IEnumerable<Event> GetEvents()
        {
            var events = new List<Event>();
            var webpage = new HtmlWeb();
            var document = webpage.Load(String.Format("{0}{1}", FED_NAVARRA_BASE_URL, CALENDAR_URL));

            var eventNodes = document.DocumentNode.SelectNodes("//div[@class='tabla']/table/tbody/tr");

            foreach (var item in eventNodes)
            {
                var cells = item.Descendants("td").ToList();
                events.Add(new Event
                {
                    EventDate = DateTime.ParseExact(cells[0].InnerText, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Name = System.Net.WebUtility.HtmlDecode(cells[1].FirstChild.InnerText),
                    Location = cells[2].InnerText,
                    Type = cells[3].InnerText,
                    ResultsUrl = GetResultsUrl(cells[5])
                });
            }
            return events;
        }

        /// <inheritDoc/>
        public Event GetExtraInfo(string detailsLink)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene la URL en la que obtener los resultados de una carrera. Explora la celda adecuada
        /// hasta obtener dicha URL.
        /// </summary>
        /// <param name="resultsCell">Celda en la que se encuentra el enlace HTML con la URL buscada.</param>
        /// <returns>La URL con los resultados de una carrera.</returns>
        private string GetResultsUrl(HtmlNode resultsCell)
        {
            var resultsLink = resultsCell.FirstChild;

            if (resultsLink == null)
                return null;

            var href = resultsLink.Attributes.Where(a => a.Name == "href").FirstOrDefault();

            if (href == null)
                return null;

            return string.Format("{0}{1}", FED_NAVARRA_BASE_URL, href.Value);

        }

        public Event GetExtraInfo(Event url)
        {
            throw new NotImplementedException();
        }
    }
}
