using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Data
{
    public class Comment : Entity
    {
        public Comment() { }

        public int Rating { get; set; }

        [NotMapped]
        public int Ranking { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime? CommentDate { get; set; }

        public Event Event { get; set; }

        [ForeignKey("Event")]
        public long EventId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public User User { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
