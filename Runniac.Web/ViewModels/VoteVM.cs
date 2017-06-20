using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Runniac.Web.ViewModels
{
    public class VoteVM
    {
        public bool Positive { get; set; }

        //public UserVM User { get; set; }

        public int UserId { get; set; }

        public long CommentId { get; set; }
    }
}
