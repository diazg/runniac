using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Runniac.Web.ViewModels
{
    public class PhotoVM
    {
        public long Id { get; set; }

        public string Url { get; set; }

        public string PhotoDate { get; set; }

        public EventVM Event { get; set; }

        public long EventId { get; set; }

        public UserVM User { get; set; }

        public int UserId { get; set; }
    }
}