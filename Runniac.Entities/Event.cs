using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data
{    
    public class Event : Entity
    {
        public Event() { }

        public string Name { get; set; }

        public DateTime? EventDate { get; set; }

        public string Location { get; set; }

        public float DistanceKms { get; set; }

        public string Type { get; set; }

        public string ResultsUrl { get; set; }

        public string Url { get; set; }

        public float Fee { get; set; }

        public string ImageUrl { get; set; }

        public bool Published { get; set; }

        public string DetailsLink { get; set; }

        public string CourseFileName { get; set; }

        [NotMapped]
        public float AvgRating { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Photo> Photos { get; set; }

    }
}
