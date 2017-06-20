using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Runniac.Web.ViewModels
{
    public class CommentVM
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        public int Rating { get; set; }

        public int Ranking { get; set; }

        public string CommentDate { get; set; }

        public long EventId { get; set; }

        public UserVM User { get; set; }

        public int UserId { get; set; }

        public IEnumerable<VoteVM> Votes { get; set; }
    }
}