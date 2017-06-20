using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runniac.Web.ViewModels
{
    public class ImportEventsVM
    {
        public IEnumerable<EventVM> Events { get; set; }
        public string Extractor { get; set; }
    }
}