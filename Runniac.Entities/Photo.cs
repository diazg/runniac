using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data
{    
    public class Photo : Entity
    {
        public string Url { get; set; }

        public DateTime PhotoDate { get; set; }

        public Event Event { get; set; }

        [ForeignKey("Event")]
        public long EventId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

    }
}
