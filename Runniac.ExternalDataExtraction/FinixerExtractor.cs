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
    public class FinixerExtractor : IEventsExtractor
    {
        private const int RADIUS_DISTANCE = 100;
        private const string BASE_URL = "http://www.finixer.com";
        private const string EVENTS_URL = "/search/list_from_navigation?currentPage={0}&searchString=running&distance={1}&location=pamplona&dates={2}";


        public IEnumerable<Data.Event> GetEvents()
        {
            var events = new List<Event>();
            var webpage = new HtmlWeb();
            var eventsFound = true;
            var currentPage = 1;

            while (eventsFound && currentPage < 3)
            {
                var document = webpage.Load(
                    String.Format(BASE_URL + EVENTS_URL, currentPage, RADIUS_DISTANCE, DateTime.Now.ToShortDateString()));
                currentPage++;

                var eventsLinks = document.DocumentNode.SelectNodes("//div[@class='right desc-long small']/a[@class='medium']");

                if (eventsLinks != null && eventsLinks.Count > 0)
                {
                    foreach (var item in eventsLinks)
                    {
                        events.Add(ReadEventInfo(item.Attributes.Where(a => a.Name == "href").FirstOrDefault().Value));                        
                    }
                }
                else
                {
                    eventsFound = false;
                }
            }

            return events;
        }

        private Event ReadEventInfo(string url)
        {
            var webpage = new HtmlWeb();
            var document = webpage.Load(String.Format("{0}{1}", BASE_URL, url));

            var name = document.DocumentNode.SelectSingleNode("//h1[@class='fs-twenty-six fw-bold left col23']/span");
            var date = document.DocumentNode.SelectSingleNode("//span[@itemprop='startDate']");
            var textDate = date != null ? date.Attributes.Where(a => a.Name == "datetime").FirstOrDefault().Value : string.Empty;
            var location = document.DocumentNode.SelectSingleNode("//span[@itemprop='addressLocality']");
            var type = document.DocumentNode.SelectSingleNode("//span[itemprop='eventType']");
            var distance = document.DocumentNode.SelectSingleNode("//p[@class='fw-bold mb-quarter no-margin']");
            var distanceKms = distance != null ? distance.Descendants("span").LastOrDefault() : null;


            return new Event
                {
                    EventDate = date != null ? ParseUtils.ParseFinixerDateFormat(textDate) : null,
                    Name = name != null ? name.InnerText.Trim() : string.Empty,
                    Location = location != null ? location.InnerText.Trim() : string.Empty,
                    Type = type != null ? type.InnerText.Trim() : string.Empty,
                    DistanceKms  = distanceKms != null ? ParseUtils.ParseFinixerDistance(distanceKms.InnerText) : 0 
                };
        }


        public Event GetExtraInfo(Event eventObj)
        {
            throw new NotImplementedException();
        }
    }
}
