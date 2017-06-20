using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Runniac.Data
{
    public class Vote : Entity
    {
        public bool Positive { get; set; }

        public int UserId { get; set; }

        public Comment Comment { get; set; }

        [ForeignKey("Comment")]
        public long CommentId { get; set; }
    }
}
