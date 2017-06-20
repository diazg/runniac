using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Runniac.Web.ViewModels
{
    public class EventVM
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string EventDate { get; set; }

        public DateTime DateWithFormat { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Range(0, 500)]
        public float DistanceKms { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        public float Fee { get; set; }

        public string Url { get; set; }

        public string ResultsUrl { get; set; }

        public string ImageUrl { get; set; }

        public bool Published { get; set; }

        public string DetailsLink { get; set; }

        public string CourseFileName { get; set; }

        public float AvgRating { get; set; }

        public TownVM Town { get; set; }

        public IEnumerable<CommentVM> Comments { get; set; }

        public IEnumerable<PhotoVM> Photos { get; set; }
    }
}