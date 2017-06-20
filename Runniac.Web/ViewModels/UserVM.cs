using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runniac.Web.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Fullname { get { return String.Format("{0} {1}", this.Name, this.Lastname); } }

        public int Points { get; set; }
    }
}