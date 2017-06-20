using HtmlAgilityPack;
using Runniac.Data;
using Runniac.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.ExternalDataExtraction
{
    public class TodoCarrerasExtractor : IEventsExtractor
    {
        private const string BASE_URL = "http://www.todocarreras.es/";
        private const string EVENTS_URL = "?search-class=DB_CustomSearch_Widget-db_customsearch_widget";


        public IEnumerable<Event> GetEvents()
        {
            var events = new List<Event>();
            var webpage = new HtmlWeb();

            var document = webpage.Load(String.Concat(BASE_URL, EVENTS_URL));
            var eventsLinks = document.DocumentNode.SelectNodes("//div[@class='post2']");

            if (eventsLinks != null && eventsLinks.Count > 0)
            {
                foreach (var item in eventsLinks)
                {
                    events.Add(ReadEventInfo(item));
                }
            }

            return events;
        }

        private Event ReadEventInfo(HtmlNode eventNode)
        {
            var name = string.Empty;
            var date = string.Empty;
            var detailsLink = string.Empty;

            var title = eventNode.SelectSingleNode("h2");
            if (title != null)
            {
                name = title.SelectSingleNode("a").InnerText;
                detailsLink = title.SelectSingleNode("a").Attributes["href"].Value;
                date = title.InnerText;
            }

            var description = eventNode.SelectNodes("*/small/b");
            var distance = string.Empty;
            var type = string.Empty;
            var fee = string.Empty;
            var location = string.Empty;

            if (description.Count > 0)
            {
                distance = description[0].InnerText;
                type = description[1].InnerText;
                fee = description[2].InnerText;
                location = description[3].InnerText;
            }

            return new Event
                {
                    EventDate = date != null ? ParseUtils.ParseTodoCarrerasDateFormat(date) : null,
                    Name = name.Trim(),
                    DetailsLink = detailsLink.Trim(),
                    Location = location.Trim(),
                    Type = type.Trim(),
                    DistanceKms = distance != null ? ParseUtils.ParseTodoCarrerasDistance(distance) : 0,
                    Fee = fee != null ? ParseUtils.ParseTodoCarrerasFee(fee) : 0
                };
        }

        /// <inheritDoc/>
        public Event GetExtraInfo(Event eventObj)
        {
            if (eventObj == null || String.IsNullOrEmpty(eventObj.DetailsLink))
                return null;

            var webpage = new HtmlWeb();
            var document = webpage.Load(eventObj.DetailsLink);

            if (document == null)
                return null;

            var eventsDiv = document.DocumentNode.SelectSingleNode("//div[@class='post']");

            if (eventsDiv == null)
                return null;

            var urlNode = eventsDiv.SelectSingleNode("//span[text() = 'Web:']/following-sibling::node()/following-sibling::node()");
            var imageNode = eventsDiv.SelectSingleNode("//div[@class='cartelIns']//img");

            eventObj.Url = urlNode != null ? urlNode.Attributes["href"].Value : string.Empty;
            eventObj.ImageUrl = imageNode != null ? imageNode.Attributes["src"].Value : string.Empty;

            return eventObj;
        }
    }
}
