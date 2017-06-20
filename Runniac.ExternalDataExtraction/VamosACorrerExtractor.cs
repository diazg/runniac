using HtmlAgilityPack;
using Runniac.Data;
using Runniac.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.ExternalDataExtraction
{
    public class VamosACorrerExtractor : IEventsExtractor
    {
        private const string BASE_URL = "http://www.vamosacorrer.com/";
        private const string EVENTS_URL = "carreras/calendario/";


        public IEnumerable<Event> GetEvents()
        {
            var events = new List<Event>();
            WebClient client = new WebClient();
            var webpage = new HtmlDocument();

            webpage.Load(client.OpenRead(String.Concat(BASE_URL, EVENTS_URL)), Encoding.Default);
            var eventsLinks = webpage.DocumentNode.SelectNodes("//div[@class='carrera clearfix']");

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
            var name = eventNode.SelectSingleNode("*/*/h2").InnerText;
            var detailsLink = eventNode.SelectSingleNode("*/a[@itemprop='url']").Attributes["href"].Value;
            var date = eventNode.SelectSingleNode("*/*/*/time").Attributes["datetime"].Value;
            var distance = eventNode.SelectSingleNode("*/*/dt[text() = 'Distancia:']");
            var location = eventNode.SelectSingleNode("*/*/dd[@itemprop='location']").InnerText;
            var imageUrl = eventNode.SelectSingleNode("*/img[@itemprop='image']").Attributes["src"].Value;


            return new Event
            {
                EventDate = date != null ? ParseUtils.ParseVamosACorrerDateFormat(date) : null,
                Name = name.Trim(),
                DetailsLink = string.Concat(BASE_URL, detailsLink.Trim()),
                Location = location.Trim(),
                DistanceKms = distance != null ? ParseUtils.ParseVamosACorrerDistance(distance.NextSibling.InnerText) : 0,
                ImageUrl = !String.IsNullOrEmpty(imageUrl) ? string.Concat(BASE_URL, imageUrl.Trim()) : null
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

            var eventsDiv = document.DocumentNode.SelectSingleNode("//div[@class='panel']/dl");

            if (eventsDiv == null)
                return null;

            var urlNode = eventsDiv.SelectSingleNode("*/a[last()]");
            eventObj.Url = urlNode != null ? urlNode.Attributes["href"].Value : string.Empty;

            return eventObj;
        }
    }
}
